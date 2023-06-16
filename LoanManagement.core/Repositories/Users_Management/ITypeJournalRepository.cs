
namespace LoanManagement.core.Repositories.Users_Management
{
	public interface ITypeJournalRepository : IRepository<TypeJournal>
	{
		Task<PagedList<TypeJournal>> GetAll(TypeJournalParameters parameters);
		Task<IEnumerable<TypeJournal>> GetAll();
		Task<TypeJournal> GetTypeJournalByCode(string code);
		Task<TypeJournal> GetTypeJournalById(int id);
	}
}
