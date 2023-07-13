namespace LoanManagement.core.Services.Loan_Management
{
	public interface IStatutMaritalService
	{
		Task<IEnumerable<StatutMarital>> GetAll();
		Task<StatutMarital> GetById(int id);
		Task<StatutMarital> Create(StatutMarital statutMarital);
		Task<StatutMarital> Update(StatutMarital updated, StatutMarital statutMarital);
		Task Delete(StatutMarital statut);
	}
}
