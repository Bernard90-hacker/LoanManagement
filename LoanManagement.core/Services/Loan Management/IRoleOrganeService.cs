namespace LoanManagement.core.Services.Loan_Management
{
	public interface IRoleOrganeService
	{
		Task<IEnumerable<RoleOrgane>> GetAll();
		Task<OrganeDecision> GetOrganeByRole(int roleId);
		Task<RoleOrgane?> GetById(int id);
		Task<RoleOrgane> Create(RoleOrgane role);
		Task<RoleOrgane> Update(RoleOrgane roleUpdated, RoleOrgane role);
		Task Delete(RoleOrgane role);	
	}
}
