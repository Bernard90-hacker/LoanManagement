using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class HabilitationProfilService : IHabilitationProfilService
	{
		private IUnitOfWork _unitOfWork;
        public HabilitationProfilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<HabilitationProfil> Create(HabilitationProfil profil)
		{
			await _unitOfWork.HabilitationProfils.AddAsync(profil);
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task Delete(HabilitationProfil profil)
		{
			_unitOfWork.HabilitationProfils.Remove(profil);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<HabilitationProfil>> GetAll(HabilitationProfilParameters parameters)
		{
			return await _unitOfWork.HabilitationProfils.GetAll(parameters);
		}

		public async Task<IEnumerable<HabilitationProfil>> GetAll()
		{
			return await _unitOfWork.HabilitationProfils.GetAll();
		}

		public async Task<HabilitationProfil?> GetHabilitationProfilById(int id)
		{
			return await _unitOfWork.HabilitationProfils.GetHabilitationProfilById(id);
		}

		public async Task<HabilitationProfil> Update(HabilitationProfil profil, HabilitationProfil habilitationProfilToBeUpdated)
		{
			habilitationProfilToBeUpdated = profil;
			await _unitOfWork.CommitAsync();

			return habilitationProfilToBeUpdated;
		}

		public async Task<HabilitationProfil> UpdateInsertion(HabilitationProfil profil, bool response)
		{
			profil.Insertion = response;
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task<HabilitationProfil> UpdateModification(HabilitationProfil profil, bool response)
		{
			profil.Modification = response;
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task<HabilitationProfil> UpdateEdition(HabilitationProfil profil, bool response)
		{
			profil.Edition = response;
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task<HabilitationProfil> UpdateSuppression(HabilitationProfil profil, bool response)
		{
			profil.Suppression = response;
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task<HabilitationProfil> UpdateGeneration(HabilitationProfil profil, bool response)
		{
			profil.Generation = response;
			await _unitOfWork.CommitAsync();

			return profil;
		}
	}
}
