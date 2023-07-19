using LoanManagement.core.Pagination.Loan_Management;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class ClientRepository : Repository<Client>, IClientRepository
	{
		private LoanManagementDbContext _context;
		public ClientRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<Client>> GetAll(ClientParameters parameters)
		{

			var customers = (await _context.Clients
				.ToListAsync()).AsQueryable();

			return PagedList<Client>.ToPagedList(
			customers, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Client>> GetAll()
		{
			return await _context.Clients.ToListAsync();
		}

		public async Task<Client?> GetById(int id)
		{
			return await _context.Clients.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<Client?> GetByIndice(int indice)
		{
			return await _context.Clients.Where(x => x.Indice == indice)
				.FirstOrDefaultAsync();
		}

		public async Task<Client?> GetByPhoneNumber(string phoneNumber)
		{
			return await _context.Clients.Where(x => x.Tel == phoneNumber)
				.FirstOrDefaultAsync();
		}

		public async Task<Compte> GetCompte(int clientId)
		{
			return await _context.Comptes.Where(x => x.ClientId == clientId)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<DossierClient>> GetDossierClient(int clientId)
		{
			return await _context.DossierClients.Where(x => x.ClientId == clientId)
				.ToListAsync();
		}
	}
}
