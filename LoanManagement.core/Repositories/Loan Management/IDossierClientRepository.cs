namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IDossierClientRepository : IRepository<DossierClient>
	{
		Task<IEnumerable<DossierClient>> GetAll();
		Task<PagedList<DossierClient>> GetAll(DossierClientParameters parameters);
		Task<DossierClient?> GetById(int Id);
		Task<IEnumerable<DossierClient?>> GetByClientId(int Id);
		Task<DossierClient?> GetByNumber(string numeroDossier);
		Task<Employeur?> GetEmployeurByDossier(int id);
		Task<StatutDossierClient?> GetStatut(int id);
		Task<Deroulement?> GetDossierDeroulement(int typePretId, double montant);
		Task<IEnumerable<InfoSanteClient>> GetInfoSanteByDossier(int dossierId);
		Task<IEnumerable<DossierClient>> GetClosed();
	}
}
