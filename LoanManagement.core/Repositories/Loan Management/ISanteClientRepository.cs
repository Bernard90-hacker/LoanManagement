namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface ISanteClientRepository : IRepository<InfoSanteClient>
	{
		Task<IEnumerable<InfoSanteClient>> GetAll();
		Task<InfoSanteClient> GetById(int id);
		Task<InfoSanteClient?> GetAnswerForQuestion(int natureQuestionId);
	}
}
