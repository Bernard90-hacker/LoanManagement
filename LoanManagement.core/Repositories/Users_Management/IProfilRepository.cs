namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IProfilRepository : IRepository<Profil>
	{
		Task<PagedList<Profil>> GetAll(ProfilParameters parameters);
		Task<IEnumerable<Profil>> GetAll();
		Task<Profil?> GetProfilById(int id);
		Task<Profil?> GetProfilByCode(string code);
	}
}
