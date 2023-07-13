using LoanManagement.core.Pagination.Loan_Management;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class EmployeurRepository : Repository<Employeur>, IEmployeurRepository
	{
		private LoanManagementDbContext _context;
		public EmployeurRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Employeur>> GetAll()
		{
			return await _context.Employeurs.ToListAsync();
		}

		public async Task<PagedList<Employeur>> GetAll(EmployeurParameters parameters)
		{
			var emp = (await _context.Employeurs
				.ToListAsync()).AsQueryable();

			return PagedList<Employeur>.ToPagedList(
			emp, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<Employeur?> GetById(int id)
		{
			return await _context.Employeurs.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
			

		public async Task<Employeur?> GetByMailBox(string mailBox)
		{
			return await _context.Employeurs.Where(x => x.BoitePostale == mailBox)
				.FirstOrDefaultAsync();
		}

		public async Task<Employeur?> GetByPhoneNumber(string number)
		{
			return await _context.Employeurs.Where(x => x.Tel == number)
				.FirstOrDefaultAsync();
		}
	}
}
