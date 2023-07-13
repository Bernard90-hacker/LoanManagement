namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IStatutMaritalRepository : IRepository<StatutMarital>
	{
		Task<IEnumerable<StatutMarital>> GetAll();
		Task<StatutMarital?> GetById(int id);
	}
}
