namespace LoanManagement.core.Services.Loan_Management
{
	public interface IOrganeDecisionService
	{
		Task<IEnumerable<OrganeDecision>> GetAll();
		Task<OrganeDecision?> GetById(int id);
		Task<OrganeDecision> Create(OrganeDecision organe);
		Task<OrganeDecision> Update(OrganeDecision organe, OrganeDecision organeUpdated);
		Task Delete(OrganeDecision organe);
		Task<IEnumerable<RoleOrgane>> GetRoles(int organeDecisionId);
	}
}
