using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Esteti.Infrastructure.Auth
{
    public class JwtManager
    {
        private readonly JwtAuthenticationOptions _jwtOptions;
        public const string UserIdClaim = "UserId";

        public JwtManager(IOptions<JwtAuthenticationOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        private SecurityKey GetSecurityKey()
        {
            if (string.IsNullOrWhiteSpace(_jwtOptions.Secret))
                throw new ArgumentException("JWT options secret is empty");

            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));
        }

        private string GenerateTokenWithClaims(IEnumerable<Claim> claims)
        {
            var mySecurityKey = GetSecurityKey();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpireInDays),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateUserToken(int userId)
        {
            var claims = new Claim[]
            {
                new Claim(UserIdClaim, userId.ToString())
            };

            return GenerateTokenWithClaims(claims);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var mySecurityKey = GetSecurityKey();

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtOptions.Audience,
                    ValidateLifetime = true,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public string? GetClaim(string token, string claimType)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (securityToken == null)
                return null;

            var stringClaimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            return stringClaimValue;
        }   

    }
}
