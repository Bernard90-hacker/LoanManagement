namespace LoanManagement.core.Services.Users_Management
{
	public interface IProfilService
	{
		Task<PagedList<Profil>> GetAll(ProfilParameters parameters);
		Task<IEnumerable<Profil>> GetAll();
		Task<Profil?> GetProfilById(int id);
		Task<Profil?> GetProfilByCode(string code);
		Task<Profil> Create(Profil profil);
		Task<Profil> Update(Profil profil, Profil profilToBeUpdated);
		Task Delete(Profil profil);
	}
}
