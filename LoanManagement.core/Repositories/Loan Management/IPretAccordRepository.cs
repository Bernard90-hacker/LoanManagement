namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IPretAccordRepository : IRepository<PretAccord>
	{
		Task<IEnumerable<PretAccord>> GetAll();
		Task<PretAccord?> GetPretAccordForDossier(int dossierId);
		Task<PretAccord?> GetById(int id);
	}
}
