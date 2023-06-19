namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class MenuRepository : Repository<Menu>, IMenuRepository
	{
		private LoanManagementDbContext _context;
        public MenuRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<Menu>> GetAll(MenuParameters parameters)
		{
			var menus = (await _context
				.Menus.ToListAsync()).AsQueryable();

			return PagedList<Menu>.ToPagedList(
				menus, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Menu>> GetAll()
		{
			return await _context.Menus.ToListAsync();
		}

		public async Task<Menu?> GetMenuByCode(string code)
		{
			return await _context.Menus.
				Where(x => x.Code == code).FirstOrDefaultAsync();
		}

		public async Task<Menu?> GetMenuById(int id)
		{
			return await _context.Menus.
				Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Menu?> GetMenuByPosition(int position)
		{
			return await _context.Menus.
				Where(x => x.Position == position).FirstOrDefaultAsync();
		}
	}
}
