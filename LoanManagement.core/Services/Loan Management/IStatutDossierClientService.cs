namespace LoanManagement.core.Services.Loan_Management
{
	public interface IStatutDossierClientService
	{
		Task<IEnumerable<StatutDossierClient>> GetAll();
		Task<StatutDossierClient?> GetById(int id);
		Task<StatutDossierClient> Create(StatutDossierClient statut);
		Task<StatutDossierClient> Update(StatutDossierClient statutUpdated, StatutDossierClient statut);
		Task Delete(StatutDossierClient statut);
		Task<StatutDossierClient> Downgrade(StatutDossierClient statut, string motif);
		Task<StatutDossierClient> Upgrade(StatutDossierClient statut);
		Task<StatutDossierClient> Reject(StatutDossierClient statut, string motif);
		Task<StatutDossierClient> Assign(StatutDossierClient statut);
	}
}
