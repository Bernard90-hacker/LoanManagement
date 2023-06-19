
using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class JournalService : IJournalService
	{
		private IUnitOfWork _unitOfWork;
        public JournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Journal> Create(Journal journal)
		{
			await _unitOfWork.Journaux.AddAsync(journal);
			return journal;
		}

		public async Task Delete(Journal journal)
		{
			_unitOfWork.Journaux.Remove(journal);
			await _unitOfWork.CommitAsync();
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
				return  _unitOfWork.Journaux.Find(x => x.UtilisateurId == userId).FirstOrDefault();

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
