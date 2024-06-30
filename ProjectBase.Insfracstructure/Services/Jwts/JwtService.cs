using Microsoft.IdentityModel.Tokens;
using ProjectBase.Domain.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectBase.Insfracstructure.Services.Jwts
{
    public class JwtService : IJwtService
    {
        private readonly AppSettingConfiguration _appConfig;

        public JwtService(AppSettingConfiguration appConfig)
        {
            _appConfig = appConfig;
        }

        public string GenerateSecurityToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JWTSection.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            string jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public DateTime GetTokenExpiry(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var expirationClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");

            if (expirationClaim != null &&
                long.TryParse(expirationClaim.Value, out long expirationUnixTime)
            )
            {
                return DateTimeOffset.FromUnixTimeSeconds(expirationUnixTime).UtcDateTime;
            }

            return DateTime.UtcNow;
        }
    }
}
