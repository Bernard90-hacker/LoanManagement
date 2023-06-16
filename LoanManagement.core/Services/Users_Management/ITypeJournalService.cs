namespace LoanManagement.core.Services.Users_Management
{
	public interface ITypeJournalService
	{
		Task<PagedList<TypeJournal>> GetAll(TypeJournalParameters parameters);
		Task<IEnumerable<TypeJournal>> GetAll();
		Task<TypeJournal> GetTypeJournalByCode(string code);
		Task<TypeJournal> GetTypeJournalById(int id);
		Task<TypeJournal> Create(TypeJournal type);
		Task<TypeJournal> Update(TypeJournal type, TypeJournal typeToBeUpdated);
		Task<TypeJournal> Delete(TypeJournal typeJournal);
	}
}
