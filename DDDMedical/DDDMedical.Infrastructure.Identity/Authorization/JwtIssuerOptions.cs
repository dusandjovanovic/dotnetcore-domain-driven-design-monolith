using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace DDDMedical.Infrastructure.Identity.Authorization
{
    public class JwtIssuerOptions
    {
        /**
         * "iss" issuer claim - iss claim identifies principal which issued the token
         */
        public string Issuer { get; set; }
        
        /**
         * "sub" subject claim - sub claim identifies the principal which is the subject of the token
         */
        public string Subject { get; set; }
        
        /**
         * "aud" audience claim - aud claim identifies recipients for which the token is for 
         */
        public string Audience { get; set; }
        
        /**
         * "iat" issued at - iat claim identifies issuing time
         */
        public DateTime IssuedAt => DateTime.UtcNow;
        
        /**
         * "nbf" not before - nbf claim identifies time before the token must not be accepted
         */
        public DateTime NotBefore => DateTime.UtcNow;
        
        /**
         * timespan for which the token will be valid for
         */
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
        
        /**
         * "exp" expiration - expiration time claim identifies the time after which the token must not be accepted
         */
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /**
         * "jti" jwt token id - claim identifying token's id
         */
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        
        /**
         * Signing key to use while generating tokens
         */
        public SigningCredentials SigningCredentials { get; set; }
    }
}