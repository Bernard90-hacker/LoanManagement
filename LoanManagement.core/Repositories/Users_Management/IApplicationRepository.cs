namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IApplicationRepository : IRepository<Application>
	{
		Task<PagedList<Application>> GetAll(ApplicationParameters parameters);
		Task<IEnumerable<Application>> GetAll();
		Task<Application?> GetApplicationByCode(string code);
		Task<IEnumerable<Application>?> GetApplicationByVersion(string version);
		Task<IEnumerable<Application>?> GetApplicationByStatus(int statut);
		Task<Application?> GetApplicationById(int id);
	}
}
