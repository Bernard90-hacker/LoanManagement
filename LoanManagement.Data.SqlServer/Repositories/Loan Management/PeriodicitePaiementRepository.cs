namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class PeriodicitePaiementRepository : Repository<PeriodicitePaiement>, IPeriodicitePaiementRepository
	{
		private LoanManagementDbContext _context;
		public PeriodicitePaiementRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<PeriodicitePaiement>> GetAll()
		{
			return await _context.PeriodicitePaiements.ToListAsync();
		}

		public async Task<PeriodicitePaiement?> GetById(int id)
		{
			return await _context.PeriodicitePaiements.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
