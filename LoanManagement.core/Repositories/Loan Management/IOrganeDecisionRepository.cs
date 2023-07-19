namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IOrganeDecisionRepository : IRepository<OrganeDecision>
	{
		Task<IEnumerable<OrganeDecision>> GetAll();
		Task<OrganeDecision?> GetById(int id);
		Task<IEnumerable<RoleOrgane>> GetRoles(int organeDecisionId);
	}
}
