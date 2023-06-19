namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class DirectionRepository : Repository<Direction>, IDirectionRepository
	{
		private LoanManagementDbContext _context;
        public DirectionRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<Direction>> GetAll(DirectionParameters parameters)
		{
			var directions = (await _context.Directions
				.ToListAsync()).AsQueryable();

			return PagedList<Direction>.ToPagedList(
			directions, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<Direction>> GetAll()
		{
			return await _context.Directions.ToListAsync();
		}

		public async Task<Direction?> GetDirectionByCode(string code)
		{
			return await _context.Directions.Where(
				x => x.Code == code).FirstOrDefaultAsync();
		}

		public async Task<Direction?> GetDirectionById(int id)
		{
			return await _context.Directions.Where(
				x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
