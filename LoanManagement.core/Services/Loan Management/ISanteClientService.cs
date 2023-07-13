namespace LoanManagement.core.Services.Loan_Management
{
	public interface ISanteClientService
	{
		Task<IEnumerable<InfoSanteClient>> GetAll();
		Task<InfoSanteClient> GetById(int id);
		Task<InfoSanteClient> GetAnswerForQuestion(int natureQuestionId);
		Task<InfoSanteClient> Create(InfoSanteClient info);
		Task<InfoSanteClient> Update(InfoSanteClient infoUpdated, InfoSanteClient info);
		Task Delete(InfoSanteClient info);
	}
}
