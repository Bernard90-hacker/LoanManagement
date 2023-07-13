namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface ICompteRepository : IRepository<Compte>
	{
		Task<PagedList<Compte>> GetAll(CompteParameters parameters);
		Task<Compte?> GetById(int id);
		Task<IEnumerable<Compte>> GetAll();
		Task<Compte?> GetByClient(int clientId);
		Task<Compte?> GetByNumber(string number);
	}
}
