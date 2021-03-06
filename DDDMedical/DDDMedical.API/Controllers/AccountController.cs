using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DDDMedical.Domain.Core.Bus;
using DDDMedical.Domain.Core.Notifications;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Infrastructure.Identity.Data;
using DDDMedical.Infrastructure.Identity.Models;
using DDDMedical.Infrastructure.Identity.Models.AccountViewModels;
using DDDMedical.Infrastructure.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDMedical.API.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _dbContext;
        private readonly IUser _user;
        private readonly IJwtFactory _jwtFactory;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            AuthDbContext dbContext,
            IUser user,
            IJwtFactory jwtFactory,
            ILoggerFactory loggerFactory,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _user = user;
            _jwtFactory = jwtFactory;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }
            
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (!signInResult.Succeeded)
            {
                NotifyError(signInResult.ToString(), "Login failure");
                return Response();
            }
            
            var appUser = await _userManager.FindByEmailAsync(model.Email);

            _logger.LogInformation(1, "User logged in.");
            return Response(await GenerateToken(appUser));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }
            
            var appUser = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var identityResult = await _userManager.CreateAsync(appUser, model.Password);
            if (!identityResult.Succeeded)
            {
                AddIdentityErrors(identityResult);
                return Response();
            }
            
            identityResult = await _userManager.AddToRoleAsync(appUser, "Admin");
            if (!identityResult.Succeeded)
            {
                AddIdentityErrors(identityResult);
                return Response();
            }
            
            var userClaims = new List<Claim>
            {
                new Claim("Consultations_Write", "Write"),
                new Claim("Consultations_Remove", "Remove"),
                new Claim("Doctors_Write", "Write"),
                new Claim("Doctors_Remove", "Remove"),
                new Claim("Patients_Write", "Write"),
                new Claim("Patients_Remove", "Remove"),
                new Claim("TreatmentRooms_Write", "Write"),
                new Claim("TreatmentRooms_Remove", "Remove"),
                new Claim("TreatmentMachines_Write", "Write"),
                new Claim("TreatmentMachines_Remove", "Remove"),
            };
            await _userManager.AddClaimsAsync(appUser, userClaims);

            // SignIn
            //await _signInManager.SignInAsync(user, false);

            _logger.LogInformation(3, "User created a new account with password.");
            return Response();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response();
            }
            
            var refreshTokenCurrent = _dbContext.RefreshTokens.SingleOrDefault
                (x => x.Token == model.RefreshToken && !x.Used && !x.Invalidated);
            if (refreshTokenCurrent is null)
            {
                NotifyError("RefreshToken", "Refresh token does not exist");
                return Response();
            }
            if (refreshTokenCurrent.ExpiryDate < DateTime.UtcNow)
            {
                refreshTokenCurrent.Invalidated = true;
                await _dbContext.SaveChangesAsync();
                NotifyError("RefreshToken", "Refresh token invalid");
                return Response();
            }
            
            var appUser = await _userManager.FindByIdAsync(refreshTokenCurrent.UserId);
            if (appUser is null)
            {
                NotifyError("User", "User does not exist");
                return Response();
            }
            
            refreshTokenCurrent.Used = true;
            await _dbContext.SaveChangesAsync();

            return Response(await GenerateToken(appUser));
        }

        [HttpGet]
        [Route("current")]
        public IActionResult GetCurrent()
        {
            return Response(new
            {
                IsAuthenticated = _user.IsAuthenticated(),
                ClaimsIdentity = _user.GetClaimsIdentity().Select(x => new { x.Type, x.Value }),
            });
        }

        private async Task<TokenViewModel> GenerateToken(ApplicationUser appUser)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, appUser.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, appUser.Id));
            
            var userClaims = await _userManager.GetClaimsAsync(appUser);
            claimsIdentity.AddClaims(userClaims);
            
            var userRoles = await _userManager.GetRolesAsync(appUser);
            claimsIdentity.AddClaims(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claimsIdentity.AddClaims(roleClaims);
            }
            
            var jwtToken = await _jwtFactory.GenerateJwtToken(claimsIdentity);
            
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                UserId = appUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(90),
                JwtId = jwtToken.JwtId
            };
            
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return new TokenViewModel
            {
                AccessToken = jwtToken.AccessToken,
                RefreshToken = refreshToken.Token,
            };
        }
    }
}