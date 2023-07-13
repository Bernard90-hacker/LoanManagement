namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface INatureQuestionRepository : IRepository<NatureQuestion>
	{
		Task<IEnumerable<NatureQuestion>> GetAll();
		Task<NatureQuestion?> GetById(int id);
	}
}
