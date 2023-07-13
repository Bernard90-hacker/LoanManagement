namespace LoanManagement.core.Services.Loan_Management
{
	public interface IMembreOrganeService
	{
		Task<IEnumerable<MembreOrgane>> GetAll();
		Task<PagedList<MembreOrgane>> GetAll(MembreOrganeParameters parameters);
		Task<IEnumerable<Utilisateur>> GetUsersByMembreOrgane(int membreOrganeId);
		Task<IEnumerable<Utilisateur>> GetUsersByOrganeDecision(int organeDecisionId);
		Task<string?> GetMembreUsername(int memberId);
		Task<MembreOrgane?> GetById(int Id);
		Task<MembreOrgane> Update(MembreOrgane membreUpdated, MembreOrgane membre);
		Task<MembreOrgane> Create(MembreOrgane membre);
		Task Delete(MembreOrgane membre);
	}
}
