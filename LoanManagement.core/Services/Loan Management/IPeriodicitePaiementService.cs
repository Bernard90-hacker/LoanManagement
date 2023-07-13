namespace LoanManagement.core.Services.Loan_Management
{
	public interface IPeriodicitePaiementService
	{
		Task<IEnumerable<PeriodicitePaiement>> GetAll();
		Task<PeriodicitePaiement> GetById(int id);
		Task<PeriodicitePaiement> Create(PeriodicitePaiement periodicite);
		Task<PeriodicitePaiement> Update(PeriodicitePaiement pUpDated, PeriodicitePaiement p);
		Task Delete(PeriodicitePaiement p);
	}
}
