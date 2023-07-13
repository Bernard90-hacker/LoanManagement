namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class OrganeDecisionRepository : Repository<OrganeDecision>, IOrganeDecisionRepository
	{
		private LoanManagementDbContext _context;
		public OrganeDecisionRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<OrganeDecision>> GetAll()
		{
			return await _context.OrganeDecisions.ToListAsync();
		}

		public async Task<OrganeDecision?> GetById(int id)
		{
			return await _context.OrganeDecisions.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
