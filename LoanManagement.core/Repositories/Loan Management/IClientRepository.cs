namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IClientRepository : IRepository<Client>
	{
		Task<PagedList<Client>> GetAll(ClientParameters parameters);
		Task<IEnumerable<Client>> GetAll();
		Task<Client?> GetById(int id);
		Task<Client?> GetByIndice(int indice);
		Task<Compte> GetCompte(int clientId);
		Task<IEnumerable<DossierClient>> GetDossierClient(int clientId);
		Task<Client?> GetByPhoneNumber(string phoneNumber);
	}
}
