namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IStatutDossierClientRepository : IRepository<StatutDossierClient>
	{
		Task<IEnumerable<StatutDossierClient>> GetAll();
		Task<StatutDossierClient?> GetById(int id);
	}
}
