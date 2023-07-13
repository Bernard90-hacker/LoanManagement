namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class StatutMaritalRepository : Repository<StatutMarital>, IStatutMaritalRepository
	{
		private LoanManagementDbContext _context;
		public StatutMaritalRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<StatutMarital>> GetAll()
		{
			return await _context.StatutMaritals.ToListAsync();
		}

		public async Task<StatutMarital?> GetById(int id)
		{
			return await _context.StatutMaritals.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
