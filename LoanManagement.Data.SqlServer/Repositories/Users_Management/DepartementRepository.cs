namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class DepartementRepository : Repository<Departement>, IDepartementRepository
	{
		private LoanManagementDbContext _context;
        public DepartementRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<Departement>> GetAll(DepartmentParameters parameters)
		{
			var depts = (await _context.Departements
				.ToListAsync()).AsQueryable();

			return PagedList<Departement>.ToPagedList(
			depts, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Departement>> GetAll()
		{
			return await _context.Departements.ToListAsync();
		}

		public async Task<Departement?> GetDepartmentByCode(string code)
		{
			return await _context.Departements.Where(
				x => x.Code == code).FirstOrDefaultAsync();
		}

		public async Task<Departement?> GetDepartmentById(int id)
		{
			return await _context.Departements.Where(
				x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
