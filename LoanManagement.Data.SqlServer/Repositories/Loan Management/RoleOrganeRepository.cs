namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class RoleOrganeRepository : Repository<RoleOrgane>, IRoleOrganeRepository
	{
		private LoanManagementDbContext _context;
		public RoleOrganeRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<RoleOrgane>> GetAll()
		{
			return await _context.RoleOrganes.ToListAsync();
		}

		public async Task<RoleOrgane?> GetById(int id)
		{
			return await _context.RoleOrganes.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
