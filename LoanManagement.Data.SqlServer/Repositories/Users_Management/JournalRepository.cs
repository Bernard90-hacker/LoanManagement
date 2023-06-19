namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class JournalRepository : Repository<Journal>, IJournalRepository
	{
		private LoanManagementDbContext _context;
        public JournalRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<Journal>> GetAll(JournalParameters parameters)
		{
			var journaux = (await _context
				.Journaux.ToListAsync()).AsQueryable();

			return PagedList<Journal>.ToPagedList(
				journaux, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Journal>> GetAll()
		{
			return await _context.Journaux.ToListAsync();
		}

		public async Task<Journal?> GetJournalById(int id)
		{
			return await _context.Journaux.
				Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
