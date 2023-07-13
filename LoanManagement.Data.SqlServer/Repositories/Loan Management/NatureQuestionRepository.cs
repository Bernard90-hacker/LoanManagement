namespace LoanManagement.Data.SqlServer.Repositories.Loan_Management
{
	public class NatureQuestionRepository : Repository<NatureQuestion>, INatureQuestionRepository
	{
		private LoanManagementDbContext _context;
		public NatureQuestionRepository(LoanManagementDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<NatureQuestion>> GetAll()
		{
			return await _context.NatureQuestions.ToListAsync();
		}

		public async Task<NatureQuestion?> GetById(int id)
		{
			return await _context.NatureQuestions.Where(x => x.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
