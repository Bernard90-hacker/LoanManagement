namespace LoanManagement.core.Services.Users_Management
{
	public interface IUtilisateurService
	{
		Task<PagedList<Utilisateur>> GetAll(UtilisateurParameters parameters);
		Task<IEnumerable<Utilisateur>> GetAll();
		Task<Utilisateur?> GetUserByUsername(string username);
		Task<Utilisateur> GetUserById(int id);
		Task<Utilisateur> Create(Utilisateur utilisateur);
		Task<Utilisateur> Update(Utilisateur utilisateur, Utilisateur utilisateurUpdate);
		Task DeleteUtilisateur(Utilisateur utilisateur);
	}
}
