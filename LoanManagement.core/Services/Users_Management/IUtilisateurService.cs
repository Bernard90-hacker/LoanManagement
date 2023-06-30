namespace LoanManagement.core.Services.Users_Management
{
	public interface IUtilisateurService
	{
		Task<PagedList<Utilisateur>> GetAll(UtilisateurParameters parameters);
		Task<IEnumerable<Utilisateur>> GetAll();
		Task<Utilisateur?> GetUserByUsername(string username);
		Task<Utilisateur?> GetUserById(int id);
		Task<Utilisateur> Create(Utilisateur utilisateur);
		Task<Utilisateur> Update(Utilisateur utilisateur, Utilisateur utilisateurUpdate);
		Task DeleteUtilisateur(Utilisateur utilisateur);
		Task<PagedList<MotDePasse>?> GetAllPasswordsForUser(int id);
		Task<bool> CheckIfPasswordHasBeenEverUsedByUser(int userId, string password);
		Task UpdateUserAccountExpiryDate(Utilisateur user, string newDate);
		Task ActivateUserAccount(Utilisateur user);
		Task DesactivateUserAccount(Utilisateur user);
		Task<PagedList<Utilisateur>> GetDisableAccounts();
		Task<PagedList<Utilisateur>> GetActivatedAccounts();
		Task<bool> DidUserInformationsMatchAllRequirements(ParamMotDePasse param, string password, string username);
		Task<string?> GetPasswordConfiguration();
		Task<Profil> GetUserProfil(Utilisateur user);
		Task Connect(Utilisateur user);
		Task Disconnect(Utilisateur user);

	}
}
