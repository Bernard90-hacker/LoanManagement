using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer.Repositories.Users_Management
{
	public class TypeJournalRepository : Repository<TypeJournal>, ITypeJournalRepository
	{
		private LoanManagementDbContext _context;
		public TypeJournalRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PagedList<TypeJournal>> GetAll(TypeJournalParameters parameters)
		{
			var types = (await _context
				.TypeJournaux.ToListAsync()).AsQueryable();

			return PagedList<TypeJournal>.ToPagedList(
				types, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<IEnumerable<TypeJournal>> GetAll()
		{
			return await _context.TypeJournaux.ToListAsync();
		}

		public async Task<TypeJournal?> GetTypeJournalByCode(string code)
		{
			return await _context.TypeJournaux.
				Where(x => x.Code == code).FirstOrDefaultAsync();
		}

		public async Task<TypeJournal?> GetTypeJournalById(int id)
		{
			return await _context.TypeJournaux
				.Where(x => x.Id == id).FirstOrDefaultAsync();
		}
	}
}
