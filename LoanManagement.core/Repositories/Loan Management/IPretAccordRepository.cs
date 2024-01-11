namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IPretAccordRepository : IRepository<PretAccord>
	{
		Task<IEnumerable<PretAccord>> GetAll();
		Task<DossierClient?> GetPretAccordForDossier(int dossierId);
		Task<PretAccord?> GetById(int id);
	}
}
