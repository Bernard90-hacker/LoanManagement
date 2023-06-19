namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IUtilisateurRepository : IRepository<Utilisateur>
	{
		Task<PagedList<Utilisateur>> GetAll(UtilisateurParameters parameters);
		Task<IEnumerable<Utilisateur>> GetAll();
		Task<Utilisateur?> GetUserByUsername(string username);
		Task<Utilisateur?> GetUserById(int id);
    }
}
