namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class StatutDossierClientRepository : Repository<StatutDossierClient>, IStatutDossierClientRepository
	{
		private LoanManagementDbContext _context;
		public StatutDossierClientRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<StatutDossierClient>> GetAll()
		{
			return await _context.StatutDossierClients.ToListAsync();
		}

		public async Task<StatutDossierClient?> GetById(int id)
		{
			return await _context.StatutDossierClients.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
