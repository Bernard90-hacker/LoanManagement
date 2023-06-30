
using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;
using Microsoft.AspNetCore.Http;
using Shyjus.BrowserDetection;

namespace LoanManagement.service.Services.Users_Management
{
	public class JournalService : IJournalService
	{
		private IUnitOfWork _unitOfWork;
		private IBrowserDetector _browserDetector;
		private HttpContextAccessor _accessor;
        public JournalService(IUnitOfWork unitOfWork, IBrowserDetector browserDetector, 
			HttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
			_browserDetector = browserDetector;
			_accessor = accessor;
        }

		public async Task<Journal> Create(Journal journal)
		{
			journal.OS = Constants.Config.ConfigConstants.GetOsName();
			journal.Navigateur = Constants.Config.ConfigConstants.GetWebBrowserName(_browserDetector);
			journal.IPAdress = Constants.Config.ConfigConstants.GetIpAddress(_accessor);
			journal.Machine = Constants.Config.ConfigConstants.GetDeviceName();
			journal.Peripherique = Constants.Config.ConfigConstants.GetWebBrowserDeviceType(_browserDetector);
			journal.Entite = "User";
			await _unitOfWork.Journaux.AddAsync(journal);
			return journal;
		}

		public async Task Delete(Journal journal)
		{
			_unitOfWork.Journaux.Remove(journal);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<Journal>> GetJournauxByType(int id)
		{
			var journaux = await _unitOfWork.Journaux.GetAll();
			var result = from j in journaux
						 where j.TypeJournalId == id
						 select j;

			return result;
		}

		public async Task<PagedList<Journal>> GetAll(JournalParameters parameters)
		{
			return await _unitOfWork.Journaux.GetAll(parameters);
		}

		public async Task<IEnumerable<Journal>> GetAll()
		{
			return await _unitOfWork.Journaux.GetAll();
		}

		public async Task<Journal?> GetJournalById(int id)
		{
			return await _unitOfWork.Journaux.GetJournalById(id);
		}

		public async Task<Journal?> GetJournalByUser(int userId)
		{
			var user = await _unitOfWork.Utilisateurs.GetUserById(userId);
			if (user is not null)
				return  _unitOfWork.Journaux.Find(x => x.UtilisateurId == userId)
					.FirstOrDefault();

			return null;
		}

		public async Task<Journal> Update(Journal journal, Journal journalToBeUpdated)
		{
			journalToBeUpdated = journal;
			await _unitOfWork.CommitAsync();

			return journalToBeUpdated;
		}
	}
}
