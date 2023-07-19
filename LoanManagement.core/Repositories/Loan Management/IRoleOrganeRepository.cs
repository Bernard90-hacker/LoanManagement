namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IRoleOrganeRepository : IRepository<RoleOrgane>
	{
		Task<IEnumerable<RoleOrgane>> GetAll();
		Task<RoleOrgane?> GetById(int id);
		Task<OrganeDecision> GetOrganeByRole(int roleId);
	}
}
