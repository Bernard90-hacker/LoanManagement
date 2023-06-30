using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoanManagement.service.Services.Users_Management
{
	public class ApplicationService : IApplicationService
	{
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Application> Create(Application app)
		{
			app.DateAjout = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.Applications.AddAsync(app);
			await _unitOfWork.CommitAsync();

			return app;
		}

		public async Task Delete(Application app)
		{
			_unitOfWork.Applications.Remove(app);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Application>> GetAll(ApplicationParameters parameters)
		{
			return await _unitOfWork.Applications.GetAll(parameters);
		}

		public async Task<IEnumerable<Application>> GetAll()
		{
			return await _unitOfWork.Applications.GetAll();
		}

		public async Task<Application?> GetApplicationByCode(string code)
		{
			return await _unitOfWork.Applications.GetApplicationByCode(code);
		}

		public async Task<Application?> GetApplicationById(int id)
		{
			return await _unitOfWork.Applications.GetApplicationById(id);
		}

		public async Task<IEnumerable<Application>?> GetApplicationByStatus(int statut)
		{
			return await _unitOfWork.Applications.GetApplicationByStatus(statut);
		}

		public  async Task<IEnumerable<Application>?> GetApplicationByVersion(string version)
		{
			return await _unitOfWork.Applications.GetApplicationByVersion(version);
		}

		public async Task<Application> Update(Application app, Application appToBeUpdated)
		{
			appToBeUpdated = app;
			await _unitOfWork.CommitAsync();

			return appToBeUpdated;
		}

		public async Task<Application> UpdateVersion(Application app, string version)
		{
			app.Version = version;
			app.DateModification = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.CommitAsync();

			return app;
		}

		public async Task<Application> UpdateStatus(Application app, int statut)
		{
			app.Statut = statut;
			app.DateModification = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.CommitAsync();

			return app;
		}

		public async Task<IEnumerable<Application>> GetApplicationModules(int applicationId)
		{
			var modules = await _unitOfWork.Applications.GetAll();
			var sousModules = modules.Where(x => x.ApplicationId == applicationId).ToList();

			return sousModules;
		}
	}
}
