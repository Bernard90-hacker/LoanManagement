using LoanManagement.core.Models.Users_Management;
using Microsoft.Data.SqlClient;

namespace LoanManagement.service.Services.Loan_Management
{
	public class ClientService : IClientService
	{
		private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }
        public async Task<Client> Create(Client client)
		{
			await _unitOfWork.Clients.AddAsync(client);
			await _unitOfWork.CommitAsync();

			return client;

		}

		public async Task Delete(Client client)
		{
			_unitOfWork.Clients.Remove(client);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Client>> GetAll(ClientParameters parameters)
		{
			return await _unitOfWork.Clients.GetAll(parameters);
		}

		public async Task<IEnumerable<Client>> GetAll()
		{
			return await _unitOfWork.Clients.GetAll();
		}

		public async Task<Client?> GetById(int id)
		{
			return await _unitOfWork.Clients.GetById(id);
		}

		public async Task<Client?> GetByIndice(int indice)
		{
			return await _unitOfWork.Clients.GetByIndice(indice);
		}

		public async Task<Compte> GetCompte(int clientId)
		{
			return await _unitOfWork.Clients.GetCompte(clientId);
		}

		public async Task<IEnumerable<DossierClient>> GetDossierClient(int clientId)
		{
			return await _unitOfWork.Clients.GetDossierClient(clientId);
		}

		public async Task<Client> Update(Client clientUpdate, Client client)
		{
			client = clientUpdate;
			await _unitOfWork.CommitAsync();

			return client;
		}

		public async Task<Client?> GetCustomerByFullName(string FullName)
		{
			var fullName = FullName.Split(" ");
			var lastName = string.Join(" ", fullName[1..]);
			var employee = await _unitOfWork.Clients.GetAll();
			var result = employee.Where(x => x.Nom == fullName[0] && x.Prenoms == string.Join(" ", fullName[1..])).FirstOrDefault();

			if (result is null) return null;

			return result;

		}

		public async Task<Client?> GetByPhoneNumber(string phoneNumber)
		{
			return await _unitOfWork.Clients.GetByPhoneNumber(phoneNumber);
		}
	}
}
