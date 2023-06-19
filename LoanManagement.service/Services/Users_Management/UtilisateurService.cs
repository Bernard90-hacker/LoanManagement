using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class UtilisateurService : IUtilisateurService
	{
		private IUnitOfWork _unitOfWork;
        public UtilisateurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Utilisateur> Create(Utilisateur utilisateur)
		{
			//utilisateur.PasswordSalt = CreatePasswordHash(utilisateur.PasswordSalt);
			await _unitOfWork.Utilisateurs.AddAsync(utilisateur);
			await _unitOfWork.CommitAsync();

			return utilisateur;
		}

		public async Task DeleteUtilisateur(Utilisateur utilisateur)
		{
			_unitOfWork.Utilisateurs.Remove(utilisateur);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Utilisateur>> GetAll(UtilisateurParameters parameters)
		{
			return await _unitOfWork.Utilisateurs.GetAll(parameters);
		}

		public async Task<IEnumerable<Utilisateur>> GetAll()
		{
			return await _unitOfWork.Utilisateurs.GetAll();
		}

		public async Task<Utilisateur> GetUserById(int id)
		{
			return await _unitOfWork.Utilisateurs.GetByIdAsync(id);
		}

		public async Task<Utilisateur?> GetUserByUsername(string username)
		{
			return await _unitOfWork.Utilisateurs.GetUserByUsername(username);
		}

		public async Task<Utilisateur> Update(Utilisateur utilisateur, Utilisateur utilisateurToBeUpdated)
		{
			utilisateurToBeUpdated = utilisateur;
			await _unitOfWork.CommitAsync();

			return utilisateurToBeUpdated;
		}
	}
}
