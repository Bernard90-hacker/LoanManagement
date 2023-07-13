namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IMembreOrganeRepository : IRepository<MembreOrgane>
	{
		Task<IEnumerable<MembreOrgane>> GetAll();
		Task<PagedList<MembreOrgane>> GetAll(MembreOrganeParameters parameters);
		Task<IEnumerable<Utilisateur>> GetUsersByMembreOrgane(int membreOrganeId);
		Task<IEnumerable<Utilisateur>> GetUsersByOrganeDecision(int organeDecisionId);
	}
}
