namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class UtilisateurRepository : Repository<Utilisateur>, IUtilisateurRepository	
	{
		private LoanManagementDbContext _context;
		public UtilisateurRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<Utilisateur>> GetAll(UtilisateurParameters parameters)
		{
			var users = (await _context
				.Utilisateurs.ToListAsync()).AsQueryable();

			return PagedList<Utilisateur>.ToPagedList(
				users, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Utilisateur>> GetAll()
		{
			return await _context.Utilisateurs.ToListAsync();
		}

		public async Task<Utilisateur?> GetUserById(int id)
		{
			return await _context.Utilisateurs
				.Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Utilisateur?> GetUserByUsername(string username)
		{
			return await _context.Utilisateurs
				.Where(x => x.Username == username).FirstOrDefaultAsync();
		}
	}
}
