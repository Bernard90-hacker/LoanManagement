namespace LoanManagement.core.Services.Users_Management
{
	public interface IApplicationService
	{
		Task<PagedList<Application>> GetAll(ApplicationParameters parameters);
		Task<IEnumerable<Application>> GetAll();
		Task<Application?> GetApplicationByCode(string code);
		Task<IEnumerable<Application>?> GetApplicationByVersion(string version);
		Task<IEnumerable<Application>?> GetApplicationByStatus(int statut);
		Task<Application?> GetApplicationById(int id);
		Task<Application> Create(Application app);
		Task<Application> Update(Application app, Application appToBeUpdated);
		Task<Application> UpdateVersion(Application app, string version);
		Task<Application> UpdateStatus(Application app, int statut);
		Task<IEnumerable<Application>> GetApplicationModules(int applicationId);
		Task Delete(Application app);
	}
}
