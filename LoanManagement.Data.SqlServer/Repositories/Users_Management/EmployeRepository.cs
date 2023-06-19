namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class EmployeRepository : Repository<Employe>,  IEmployeRepository
	{
		private LoanManagementDbContext _context;
        public EmployeRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<IEnumerable<Employe>> GetAll()
		{
			return await _context.Employes.ToListAsync();
		}

		public async Task<PagedList<Employe>> GetAll(EmployeParameters parameters)
		{
			var employes = (await _context.Employes.
				ToListAsync()).AsQueryable();

			return PagedList<Employe>.ToPagedList(
				employes, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<Employe?> GetEmployeByEmail(string email)
		{
			return await _context.Employes
				.Where(x => x.Email == email).FirstOrDefaultAsync();
		}

		public async Task<Employe?> GetEmployeById(int id)
		{
			return await _context.Employes
				.Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
