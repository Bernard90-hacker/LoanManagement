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

		public async Task<Profil> UpdateProfilExpiryDate(Profil profil, string expiryDate)
		{
			profil.DateExpiration = expiryDate;
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task<Profil> UpdateProfilStatus(Profil profil, int statut)
		{
			profil.Statut = statut;
			await _unitOfWork.CommitAsync();

			return profil;
		}

		public async Task<PagedList<Profil>> GetDisabledProfiles()
		{
			var profiles = await _unitOfWork.Profils.GetAll();
			var result = profiles.Where(x => x.Statut == 2).ToList().AsQueryable();
			var pageNumber = new ProfilParameters().PageNumber;
			var pageSize = new ProfilParameters().PageSize;

			return PagedList<Profil>.ToPagedList(result, pageNumber, pageSize);
		}

		public async Task<PagedList<Profil>> GetActivatedProfiles()
		{
			var profiles = await _unitOfWork.Profils.GetAll();
			var result = profiles.Where(x => x.Statut == 1).ToList().AsQueryable();
			var pageNumber = new ProfilParameters().PageNumber;
			var pageSize = new ProfilParameters().PageSize;

			return PagedList<Profil>.ToPagedList(result, pageNumber, pageSize);
		}

		public async Task<HabilitationProfil?> GetProfilHabilitation(int id)
		{
			var habilitations = await _unitOfWork.HabilitationProfils.GetAll();

			return habilitations.Where(x => x.ProfilId == id).FirstOrDefault();
		}

		public async Task<Profil> GetUserProfil(Utilisateur user)
		{
			var profils = await _unitOfWork.Profils.GetAll();
			

			return profils.Where(x => x.UtilisateurId == user.Id).First();
		}
	}
}
