namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class ParamMotDePasseRepository : Repository<ParamGlobal>, IParamMotDePasseRepository
	{
        private LoanManagementDbContext _context;
        public ParamMotDePasseRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<ParamGlobal>> GetAll(ParamMotDePasseParameters parameters)
		{
			var passwordsParam = (await _context
				.ParamGlobals.ToListAsync()).AsQueryable();

			return PagedList<ParamGlobal>.ToPagedList (
				passwordsParam, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<ParamGlobal>> GetAll()
		{
			return await _context.ParamGlobals.ToListAsync();
		}

		public async Task<ParamGlobal?> GetById(int id)
		{
			return await _context
				.ParamGlobals.Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<ParamGlobal?> GetCurrentParameter()
		{
			var param = await _context.ParamGlobals.ToListAsync();
			var result = param.FirstOrDefault();
			if (result is null) return null;

			return result;
		}
	}
}
