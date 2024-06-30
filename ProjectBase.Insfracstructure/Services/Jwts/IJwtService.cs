using System.Security.Claims;

namespace ProjectBase.Insfracstructure.Services.Jwts
{
    public interface IJwtService
    {
        string GenerateSecurityToken(List<Claim> claims);
        DateTime GetTokenExpiry(string token);
    }
}