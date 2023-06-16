using LoanManagement.core;
using LoanManagement.core.Models;
using LoanManagement.core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoanManagement.service.Services
{
	public class UserTokenService : IUserTokenService
	{
		private IUnitOfWork _unitOfWork;
		public UserTokenService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

		public async Task<UserToken> Add(UserToken token)
		{
			await _unitOfWork.UserTokens.AddAsync(token);
			await _unitOfWork.CommitAsync();

			return token;
		}

		public string CreateAccessToken(int id, string AccessToken)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.NameIdentifier, id.ToString())
			};
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(AccessToken));
			SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken? token = new(
					notBefore: DateTime.Now,
					expires: DateTime.Now.AddSeconds(30),
					signingCredentials: creds,
					claims : claims
				);

			string? jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}

		public string CreateRefreshToken(int id, string refreshToken)
		{
			List<Claim> claim = new()
			{
				new Claim(ClaimTypes.NameIdentifier, id.ToString())
			};
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(refreshToken));
			SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken? token = new(
					notBefore: DateTime.Now,
					expires: DateTime.Now.AddDays(7),
					signingCredentials : creds,
					claims : claim
				);

			string? jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;

		}

		public int DecodeToken(string? token, out bool hasTokenExpired)
		{
			JwtSecurityToken jwtToken = new(token);
			int id = int.Parse(jwtToken.Claims.First(
				claim => ClaimTypes.NameIdentifier == claim.Type).Value);

			hasTokenExpired = jwtToken.ValidTo < DateTime.UtcNow;
			return id;
		}

		public async Task Delete(UserToken token)
		{
			_unitOfWork.UserTokens.Remove(token);
			await _unitOfWork.CommitAsync();
		}
	}
}
