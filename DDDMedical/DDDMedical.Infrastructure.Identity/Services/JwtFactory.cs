using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DDDMedical.Infrastructure.Identity.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DDDMedical.Infrastructure.Identity.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            _jwtIssuerOptions = jwtIssuerOptions.Value;
            ThrowIfInvalidOptions(_jwtIssuerOptions);
        }

        public async Task<JwtToken> GenerateJwtToken(ClaimsIdentity claimsIdentity)
        {
            claimsIdentity.AddClaims(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            });

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _jwtIssuerOptions.Issuer,
                    Audience =  _jwtIssuerOptions.Audience,
                    Subject = claimsIdentity,
                    NotBefore = _jwtIssuerOptions.NotBefore,
                    Expires = _jwtIssuerOptions.Expiration,
                    SigningCredentials = _jwtIssuerOptions.SigningCredentials
                }
            );

            return new JwtToken
            {
                JwtId = token.Id,
                AccessToken = tokenHandler.WriteToken(token)
            };
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be non-zero timespan", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long) Math.Round(
                (date.ToUniversalTime() - 
                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }

    public class JwtToken
    {
        public string JwtId { get; set; }
        public string AccessToken { get; set; }
    }
}