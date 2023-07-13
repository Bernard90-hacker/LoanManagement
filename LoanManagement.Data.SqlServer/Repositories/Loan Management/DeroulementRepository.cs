using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class DeroulementRepository : Repository<Deroulement>, IDeroulementRepository
	{
		private LoanManagementDbContext _context;
		public DeroulementRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Deroulement>> GetAll()
		{
			return await _context.Deroulements.ToListAsync();
		}

		public async Task<Deroulement?> GetById(int id)
		{
			return await _context.Deroulements.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<EtapeDeroulement>> GetSteps(int deroulementId)
		{
			return await _context.EtapeDeroulements.Where(x => x.DeroulementId == deroulementId)
				.ToListAsync();
		}
	}
}
