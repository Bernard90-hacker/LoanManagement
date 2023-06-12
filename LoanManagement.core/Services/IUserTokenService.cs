namespace LoanManagement.core.Services
{
	public interface IUserTokenService
	{
		public string CreateAccessToken(int id, string accessToken);
		public string CreateRefreshToken(int id, string refreshToken);
		public int DecodeToken(string? token, out bool hasTokenExpired);
		Task<UserToken> Add(UserToken token);
		Task Delete(UserToken token);
	}
}
