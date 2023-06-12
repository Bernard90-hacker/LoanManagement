namespace LoanManagement.Data.SqlServer.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private LoanManagementDbContext _context;
		public UserRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<User>> GetAll(UserParameters parameters)
		{
			var users = (await _context.Users
				.ToListAsync()).AsQueryable();
			
			return PagedList<User>.ToPagedList(
			users, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<bool> GetUser(string email)
		{
			var users = (await _context.Users
				.ToListAsync())
				.AsQueryable();

			return _context
				.Users.Any(u => u.Email.Equals(email));

		}

		public async Task<User?> GetUser(int Id)
		{

			return (await _context.Users.ToListAsync())
				.Where(u => u.Id.Equals(Id)).FirstOrDefault();
		}

		public async Task<User?> Login(LoginDto dto)
		{
			var user = (await _context.Users.ToListAsync()).Where(u => u.Email == dto.Email && 
			Constants.Utils.UtilsConstant.HashPassword(dto.Password) == u.Password).FirstOrDefault();

			return user;
		}

		public void SearchByParameters(ref IQueryable<User> user, string email, string password)
		{
			if (!user.Any())
				return;

			var predicates = new List<Predicate<User>>();
			if (!string.IsNullOrWhiteSpace(email))
				predicates.Add(a => a.Email.Equals(email.Trim().ToLower()));
			if (!string.IsNullOrWhiteSpace(password))
				predicates.Add(a => a.Password.Equals(password.Trim().ToLower()));

			user = (user.FindAll(predicates)).AsQueryable();
		}
	}
}
