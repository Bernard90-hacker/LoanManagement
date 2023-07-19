using Azure.Core;
using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;
using Microsoft.EntityFrameworkCore.Storage;

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

		public async Task<Utilisateur?> GetUserById(int id)
		{
			return await _unitOfWork.Utilisateurs.GetUserById(id);
		}

		public async Task<Utilisateur?> GetUserByUsername(string username)
		{
			return await _unitOfWork.Utilisateurs.GetUserByUsername(username);
		}

		public async Task<PagedList<MotDePasse>?> GetAllPasswordsForUser(int id)
		{

			var passwords = _unitOfWork.MotDePasses.Find(x => x.UtilisateurId == id).ToList().AsQueryable();
			var pageNumber = new UtilisateurParameters().PageNumber;
			var pageSize = new UtilisateurParameters().PageSize;
			return PagedList<MotDePasse>.ToPagedList(passwords, pageNumber, pageSize);
		}

		public async Task<Utilisateur> Update(Utilisateur utilisateur, Utilisateur utilisateurToBeUpdated)
		{
			utilisateur.PasswordHash = utilisateurToBeUpdated.PasswordHash;
			utilisateur.PasswordSalt = utilisateurToBeUpdated.PasswordSalt;
			utilisateur.Username = utilisateurToBeUpdated.Username;
			utilisateur.DateModificationMotDePasse = utilisateur.DateModificationMotDePasse;
			await _unitOfWork.CommitAsync();

			return utilisateur;
		}

		public async Task<bool> CheckIfPasswordHasBeenEverUsedByUser(int userId, string password)
		{
			var result = false;
			var userPasswords = await GetAllPasswordsForUser(userId);
			foreach (var item in userPasswords)
			{
				if (Constants.Utils.UtilsConstant.CheckPassword(password, item.OldPasswordHash, item.OldPasswordSalt))
				{
					result = true;
					break;
				}
			}

			return result;
		}

		public async Task UpdateUserAccountExpiryDate(Utilisateur user, string newDate)
		{
			user.DateExpirationCompte = newDate;
			await _unitOfWork.CommitAsync();
		}

		public async Task DesactivateUserAccount(Utilisateur user)
		{
			user.Statut = 2;
			user.DateDesactivation = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.CommitAsync();
		}

		public async Task ActivateUserAccount(Utilisateur user)
		{
			user.Statut = 1;
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Utilisateur>> GetActivatedAccounts()
		{
			var accounts = await _unitOfWork.Utilisateurs.GetAll();
			var result = accounts.Where(x => x.Statut == 1).ToList().AsQueryable();
			var pageNumber = new UtilisateurParameters().PageNumber;
			var pageSize = new UtilisateurParameters().PageSize;

			return PagedList<Utilisateur>.ToPagedList(result, pageNumber, pageSize);
		}

		public async Task<PagedList<Utilisateur>> GetDisableAccounts()
		{
			var accounts = await _unitOfWork.Utilisateurs.GetAll();
			var result = accounts.Where(x => x.Statut == 2).ToList().AsQueryable();
			var pageNumber = new UtilisateurParameters().PageNumber;
			var pageSize = new UtilisateurParameters().PageSize;

			return PagedList<Utilisateur>.ToPagedList(result, pageNumber, pageSize);
		}

		private static bool HasSpecialChars(string yourString)
		{
			return yourString.Any(ch => !char.IsLetterOrDigit(ch));
		}

		public async Task<bool> DidUserInformationsMatchAllRequirements(ParamGlobal param, string password, string username)
		{
			var currentParam = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
			if (currentParam.IncludeUpperCase)
			{
				if (!password.Any(c => char.IsUpper(c))) return false;
			}
			if (currentParam.IncludeLowerCase)
			{
				if (!password.Any(c => char.IsLower(c))) return false;
			}
			if (currentParam.IncludeSpecialCharacters)
			{
				if (!HasSpecialChars(password)) return false;
			}
			if (currentParam.IncludeDigits)
			{
				if (!password.Any(c => char.IsLetterOrDigit(c))) return false;
			}
			if (currentParam.ExcludeUsername)
			{
				if(password.ToLower().Contains(username.ToLower())) return false;
			}
			if (password.Length < param.Taille) return false;
			return true;
		}

		public static bool MustPasswordContainsUsername(ParamGlobal param, string username, string password)
		{
			var result = false;
			if (param.ExcludeUsername)
			{
				if (password.ToLower().Contains(username.ToLower())) result = false;
				else result = true;
			}

			return result;
		}

		public async Task<string?> GetPasswordConfiguration()
		{
			var param = await _unitOfWork.ParamMotDePasses.GetCurrentParameter();
			var configuration = "";
			if (param is null) return null;
			if (param.IncludeDigits)
				configuration += "Le mot de passe doit contenir des chiffres\n";
			if (param.IncludeLowerCase)
				configuration += "Le mot de passe doit contenir des caractères minuscules\n";
			if (param.IncludeUpperCase)
				configuration += "Le mot de passe doit contenir des caractères majuscules\n";
			if (param.ExcludeUsername)
				configuration += "Le mot de passe ne doit pas contenir le nom d'utilisateur\n";

			configuration += $"Le mot de passe doit contenir au moins {param.Taille} caractères";

			return configuration;
		}

		public async Task<Profil> GetUserProfil(Utilisateur user)
		{
			return
				await _unitOfWork.Profils.GetProfilById((int)user.ProfilId);

		}
		
		public async Task Connect(Utilisateur user)
		{
			user.IsConnected = true;
			await _unitOfWork.CommitAsync();
		}

		public async Task Disconnect(Utilisateur user)
		{
			user.IsConnected = false;
			await _unitOfWork.CommitAsync();
		}
	}
}
