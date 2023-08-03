using System.Security.Claims;

namespace LoanManagement.core.Services.Users_Management
{
    public interface ITokenService
    {
        string GenerateAccessToken(string secret, IEnumerable<Claim> claims, string issuer = null, string audience = null);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string secret, string token);
    }
}
