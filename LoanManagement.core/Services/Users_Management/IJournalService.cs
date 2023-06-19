namespace LoanManagement.core.Services.Users_Management
{
	public interface IJournalService
	{
		Task<PagedList<Journal>> GetAll(JournalParameters parameters);
		Task<IEnumerable<Journal>> GetAll();
		Task<Journal?> GetJournalById(int id);
		Task<Journal?> GetJournalByUser(int userId);
		Task<Journal> Create(Journal journal);
		Task<Journal> Update(Journal journal, Journal journalToBeUpdated);
		Task Delete(Journal journal);
	}
}
