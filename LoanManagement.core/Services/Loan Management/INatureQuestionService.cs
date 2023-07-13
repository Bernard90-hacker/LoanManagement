namespace LoanManagement.core.Services.Loan_Management
{
	public interface INatureQuestionService
	{
		Task<IEnumerable<NatureQuestion>> GetAll();
		Task<NatureQuestion> GetById(int id);
		Task<NatureQuestion> Create(NatureQuestion nature);
		Task Delete(NatureQuestion nature);
		Task<NatureQuestion> Update(NatureQuestion natureUpdated, NatureQuestion nature);
	}
}
