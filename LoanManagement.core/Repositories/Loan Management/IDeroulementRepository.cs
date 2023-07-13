namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IDeroulementRepository : IRepository<Deroulement>
	{
		Task<IEnumerable<Deroulement>> GetAll();
		Task<Deroulement?> GetById(int id);
		Task<IEnumerable<EtapeDeroulement>> GetSteps(int deroulementId);
	}
}
