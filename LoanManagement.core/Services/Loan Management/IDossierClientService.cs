namespace LoanManagement.core.Services.Loan_Management
{
	public interface IDossierClientService
	{
		Task<IEnumerable<DossierClient>> GetAll();
		Task<PagedList<DossierClient>> GetAll(DossierClientParameters parameters);
		Task<DossierClient?> GetById(int Id);
		Task<DossierClient?> GetByNumber(string numeroDossier);
		Task Cloturer(DossierClient dossierClient);
		Task<Employeur?> GetEmployeurByDossier(int id);
		Task<Deroulement?> GetDossierDeroulement(int typePretId, double montant);
		Task<StatutDossierClient?> GetStatut(int id);
		Task<DossierClient> Create(DossierClient dossier);
		Task<DossierClient> Update(DossierClient dossierClientUpdated, DossierClient dossierClient);
		Task Delete(DossierClient dossierClient);
		Task<IEnumerable<InfoSanteClient>> GetInfoSanteByDossier(int dossierId);
		Task<IEnumerable<DossierClient>> GetClosed();
		Task AddCouverture(DossierClient dossierClient, string couverture);
		Task<DossierClient?> GetByClientId(int Id);


	}
}
