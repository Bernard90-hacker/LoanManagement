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

		public async Task<OrganeDecision> GetOrganeByRole(int roleId)
		{
			var role = await _context.RoleOrganes.Where(x => x.Id == roleId)
				.FirstOrDefaultAsync();
			if (role is null) throw new ArgumentException($"Aucun role n'ayant l'id " +
				$"{roleId} n'a été trouvé");
			var result = (from x in (await _context.OrganeDecisions.ToListAsync())
						 where role.OrganeDecisionId == x.Id
						 select x).FirstOrDefault();

			return result;
		}
	}
}
