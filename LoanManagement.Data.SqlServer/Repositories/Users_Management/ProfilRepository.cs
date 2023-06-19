namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class ProfilRepository : Repository<Profil>, IProfilRepository
	{
		private LoanManagementDbContext _context;
		public ProfilRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<Profil>> GetAll(ProfilParameters parameters)
		{
			var profils = (await _context
				.Profils.ToListAsync()).AsQueryable();

			return PagedList<Profil>.ToPagedList(
				profils, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Profil>> GetAll()
		{
			return await _context.Profils.ToListAsync();
		}

		public async Task<Profil?> GetProfilByCode(string code)
		{
			return await _context.Profils.Where(x => x.Code == code).FirstOrDefaultAsync();
		}

		public async Task<Profil?> GetProfilById(int id)
		{
			return await _context.Profils.Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
