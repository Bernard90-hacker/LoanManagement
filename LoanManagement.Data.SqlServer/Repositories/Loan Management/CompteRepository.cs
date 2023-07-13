using LoanManagement.core.Pagination.Loan_Management;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class CompteRepository : Repository<Compte>, ICompteRepository
	{
		private LoanManagementDbContext _context;
		public CompteRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<Compte>> GetAll(CompteParameters parameters)
		{
			var comptes = (await _context.Comptes
				.ToListAsync()).AsQueryable();

			return PagedList<Compte>.ToPagedList(
			comptes, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Compte>> GetAll()
		{
			return await _context.Comptes.ToListAsync();
		}

		public async Task<Compte?> GetByClient(int clientId)
		{
			return await _context.Comptes.Where(x => x.ClientId == clientId)
				.FirstOrDefaultAsync();
		}

		public async Task<Compte?> GetById(int id)
		{
			return await _context.Comptes.Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Compte?> GetByNumber(string number)
		{
			return await _context.Comptes.Where(x => x.NumeroCompte == number)
				.FirstOrDefaultAsync();
		}
	}
}
