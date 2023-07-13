namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IPeriodicitePaiementRepository : IRepository<PeriodicitePaiement>
	{
		Task<IEnumerable<PeriodicitePaiement>> GetAll();
		Task<PeriodicitePaiement?> GetById(int id);
	}
}
