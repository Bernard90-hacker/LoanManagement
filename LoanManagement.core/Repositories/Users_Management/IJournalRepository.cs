namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IJournalRepository : IRepository<Journal>
	{
		Task<PagedList<Journal>> GetAll(JournalParameters parameters);
		Task<IEnumerable<Journal>> GetAll();
		Task<Journal> GetJournalById(int id);
		Task<Journal> GetJournalByUser(int userId);

	}
}
