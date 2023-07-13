namespace LoanManagement.core.Services.Loan_Management
{
	public interface ICompteService
	{
		Task<PagedList<Compte>> GetAll(CompteParameters parameters);
		Task<Compte?> GetById(int id);
		Task<IEnumerable<Compte>> GetAll();
		Task<Compte?> GetByClient(int clientId);
		Task<Compte?> GetByNumber(string accountNumber);
		Task<Compte> Create(Compte compte);
		Task<Compte> IncreaseAmount(Compte compte, double solde);
		Task<Compte> DecreaseAmount(Compte compte, double solde);
		Task Delete(Compte compte);
	}
}
