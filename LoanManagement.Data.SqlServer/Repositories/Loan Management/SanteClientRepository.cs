using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class SanteClientRepository : Repository<InfoSanteClient>, ISanteClientRepository
	{
		private LoanManagementDbContext _context;
		public SanteClientRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<InfoSanteClient>> GetAll()
		{
			return await _context.InfoSanteClients.ToListAsync();
		}

		public async Task<InfoSanteClient?> GetAnswerForQuestion(int natureQuestionId)
		{
			return await _context.InfoSanteClients.Where(x => x.NatureQuestionId == natureQuestionId)
				.FirstOrDefaultAsync();
		}

		public async Task<InfoSanteClient?> GetById(int id)
		{
			return await _context.InfoSanteClients
				.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
