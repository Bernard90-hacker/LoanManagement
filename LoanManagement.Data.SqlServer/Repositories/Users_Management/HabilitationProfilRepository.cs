namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class HabilitationProfilRepository : Repository<HabilitationProfil>, IHabilitationProfilRepository
	{
		private LoanManagementDbContext _context;
        public HabilitationProfilRepository(LoanManagementDbContext context) : base(context)
        {
            _context = context;
        }

		public async Task<PagedList<HabilitationProfil>> GetAll(HabilitationProfilParameters parameters)
		{
			var habilitations = (await _context.HabilitationProfils
				.ToListAsync()).AsQueryable();

			return PagedList<HabilitationProfil>.ToPagedList(
				habilitations, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<HabilitationProfil>> GetAll()
		{
			return await _context.HabilitationProfils
				.ToListAsync();
		}

		public async Task<HabilitationProfil?> GetHabilitationProfilById(int id)
		{
			return await _context.HabilitationProfils
				.Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
