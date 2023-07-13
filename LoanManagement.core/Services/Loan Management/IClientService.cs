namespace LoanManagement.core.Services.Loan_Management
{
	public interface IClientService
	{
		Task<PagedList<Client>> GetAll(ClientParameters parameters);
		Task<IEnumerable<Client>> GetAll();
		Task<Client?> GetById(int id);
		Task<Client?> GetByIndice(int indice);
		Task<IEnumerable<Compte>> GetComptes(int clientId);
		Task<IEnumerable<DossierClient>> GetDossierClient(int clientId);
		Task<Client?> GetCustomerByFullName(string FullName);
		Task<Client?> GetByPhoneNumber(string phoneNumber);
		Task<Client> Create(Client client);
		Task Delete(Client client);
		Task<Client> Update(Client clientUpdate, Client client);

	}
}
