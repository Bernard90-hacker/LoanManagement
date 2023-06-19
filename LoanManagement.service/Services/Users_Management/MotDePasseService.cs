using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class MotDePasseService : IMotDePasseService
	{
		private IUnitOfWork _unitOfWork;
        public MotDePasseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<PagedList<MotDePasse>> GetAll(MotDePasseParameters parameters)
		{
			return await _unitOfWork.MotDePasses.GetAll(parameters);
		}

		public async Task<IEnumerable<MotDePasse>> GetAll()
		{
			return await _unitOfWork.MotDePasses.GetAll();
		}

		public async Task<MotDePasse?> GetPasswordById(int id)
		{
			return await _unitOfWork.MotDePasses.GetPasswordsById(id);
		}

		public async Task<IEnumerable<MotDePasse>?> GetPasswordsByHash(string hash)
		{
			return await _unitOfWork.MotDePasses.GetPasswordsByHash(hash);
		}
	}
}
