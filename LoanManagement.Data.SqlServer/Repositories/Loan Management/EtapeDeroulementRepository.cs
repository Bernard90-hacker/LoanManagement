namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class EtapeDeroulementRepository : Repository<EtapeDeroulement>, IEtapeDeroulementRepository
	{
		private readonly LoanManagementDbContext _context;	
		public EtapeDeroulementRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<EtapeDeroulement>> GetAll()
		{
			return await _context.EtapeDeroulements.ToListAsync();
		}

		public async Task<EtapeDeroulement?> GetById(int Id)
		{
			return await _context.EtapeDeroulements.Where(x => x.Id == Id)
				.FirstOrDefaultAsync();
		}
	}
}
