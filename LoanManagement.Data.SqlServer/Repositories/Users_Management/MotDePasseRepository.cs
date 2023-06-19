namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class MotDePasseRepository : Repository<MotDePasse>, IMotDePasseRepository
	{
		private LoanManagementDbContext _context;
        public MotDePasseRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<MotDePasse>> GetAll(MotDePasseParameters parameters)
		{
			var passwords = (await _context
				.MotDePasses.ToListAsync()).AsQueryable();

			return PagedList<MotDePasse>.ToPagedList(
				passwords, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<MotDePasse>> GetAll()
		{
			return await _context.MotDePasses.ToListAsync();
		}

		public async Task<PagedList<MotDePasse>?> GetPasswordsByHash(string hash)
		{
			var pageNumber = new MotDePasseParameters().PageNumber;
			var pageSize = new MotDePasseParameters().PageSize;

			var passwords = _context
				.MotDePasses.Where(x => x.OldPasswordHash == hash).AsQueryable();

			return PagedList<MotDePasse>.ToPagedList(
				passwords, pageNumber, pageSize);
		}

		public async Task<MotDePasse?> GetPasswordsById(int id)
		{
			return await _context.MotDePasses.
				Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
