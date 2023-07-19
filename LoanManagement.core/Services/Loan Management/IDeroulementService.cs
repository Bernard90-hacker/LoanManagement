namespace LoanManagement.core.Services.Loan_Management
{
	public interface IDeroulementService
	{
		Task<IEnumerable<Deroulement>> GetAll();
		Task<Deroulement?> GetById(int id);
		Task<IEnumerable<EtapeDeroulement>> GetSteps(int deroulementId);

		Task<Deroulement> Create(Deroulement deroulement);
		Task<Deroulement> Update(Deroulement deroulement, Deroulement deroulementUpdated);
		Task Delete(Deroulement deroulement);
	}
}
