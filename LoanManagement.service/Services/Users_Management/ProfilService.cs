using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class ProfilService : IProfilService
	{
		private IUnitOfWork _unitOfWork;
        public ProfilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Profil> Create(Profil profil)
		{
			await _unitOfWork.Profils.AddAsync(profil);
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task Delete(Profil profil)
		{
			_unitOfWork.Profils.Remove(profil);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Profil>> GetAll(ProfilParameters parameters)
		{
			return await _unitOfWork.Profils.GetAll(parameters);
		}

		public async Task<IEnumerable<Profil>> GetAll()
		{
			return await _unitOfWork.Profils.GetAll();
		}

		public async Task<Profil?> GetProfilByCode(string code)
		{
			return await _unitOfWork.Profils.GetProfilByCode(code);
		}

		public async Task<Profil?> GetProfilById(int id)
		{
			return await _unitOfWork.Profils.GetProfilById(id);
		}

		public async Task<Profil> Update(Profil profil, Profil profilToBeUpdated)
		{
			profilToBeUpdated = profil;
			await _unitOfWork.CommitAsync();

			return profilToBeUpdated;
		}
	}
}
