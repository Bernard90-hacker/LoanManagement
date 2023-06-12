using System.IdentityModel.Tokens.Jwt;

namespace LoanManagement.Data.SqlServer.Repositories
{
	public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
	{
		private LoanManagementDbContext _context;
		public UserTokenRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}
		public string CreateAccessToken(int id, string accessToken)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, id.ToString())
			};
			SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(accessToken));
			SigningCredentials? creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken token = new (
				claims : claims,
				notBefore: DateTime.Now,
				expires : DateTime.Now.AddSeconds(30),
				signingCredentials : creds
			);

			string? jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		public string CreateRefreshToken(int id, string refreshToken)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, id.ToString())
			};
			SymmetricSecurityKey? key = new(Encoding.UTF8.GetBytes(refreshToken));
			SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken token = new(
					claims: claims,
					notBefore: DateTime.Now,
					expires: DateTime.Now.AddDays(7),
					signingCredentials: creds
				);

			string? jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		public int DecodeToken(string? token, out bool hasTokenExpired)
		{
			token = token.ToString();
			JwtSecurityToken? jwt = new(token);

			int id = int.Parse(jwt.Claims.First(
				claims => ClaimTypes.NameIdentifier == claims.Type).Value);
			hasTokenExpired = jwt.ValidTo < DateTime.UtcNow;
			return id;
		}

		public async Task<PagedList<UserToken>> GetAll(UserTokenParameters parameters)
		{
			var tokens = (await _context.UserTokens
				.ToListAsync()).AsQueryable();

			return PagedList<UserToken>.ToPagedList(
			tokens, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<UserToken>> GetAll()
		{
			return await _context.UserTokens.ToListAsync();
		}
	}
}
