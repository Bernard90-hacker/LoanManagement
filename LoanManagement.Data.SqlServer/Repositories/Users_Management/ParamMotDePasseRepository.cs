namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class ParamMotDePasseRepository : Repository<ParamMotDePasse>, IParamMotDePasseRepository
	{
        private LoanManagementDbContext _context;
        public ParamMotDePasseRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<ParamMotDePasse>> GetAll(ParamMotDePasseParameters parameters)
		{
			var passwordsParam = (await _context
				.ParamMotDePasses.ToListAsync()).AsQueryable();

			return PagedList<ParamMotDePasse>.ToPagedList (
				passwordsParam, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<ParamMotDePasse>> GetAll()
		{
			return await _context.ParamMotDePasses.ToListAsync();
		}

		public async Task<ParamMotDePasse?> GetById(int id)
		{
			return await _context
				.ParamMotDePasses.Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
