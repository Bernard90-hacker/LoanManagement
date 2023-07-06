using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoanManagement.service.Services.Users_Management
{
	public class TokenService
	{
		public TokenService() { }

		public string CreateAccessToken(string username, string role, string accessToken)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.NameIdentifier, username),
				new Claim(ClaimTypes.Role, role)
			};

			SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(accessToken));
			SigningCredentials? creds = new(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken? token = new(
				claims: claims,
				notBefore: DateTime.Now,
				expires: DateTime.Now.AddSeconds(30),
				signingCredentials: creds);

			string? jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		public static string DecodeToken(string? token, out bool hasTokenExpired)
		{
			token = token.ToString();
			JwtSecurityToken? jwtToken = new(token);
			string username = jwtToken.Claims.Where
				(claim => ClaimTypes.NameIdentifier == claim.Type).First().ToString();
			hasTokenExpired = jwtToken.ValidTo < DateTime.UtcNow;

			return username;
		}
	}
}
