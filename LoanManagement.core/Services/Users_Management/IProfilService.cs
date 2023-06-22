namespace LoanManagement.core.Services.Users_Management
{
	public interface IProfilService
	{
		Task<PagedList<Profil>> GetAll(ProfilParameters parameters);
		Task<IEnumerable<Profil>> GetAll();
		Task<PagedList<Profil>> GetActivatedProfiles();
		Task<PagedList<Profil>> GetDisabledProfiles();
		Task<HabilitationProfil?> GetProfilHabilitation(int id);
		Task<Profil?> GetProfilById(int id);
		Task<Profil?> GetProfilByCode(string code);
		Task<Profil> Create(Profil profil);
		Task<Profil> Update(Profil profil, Profil profilToBeUpdated);
		Task Delete(Profil profil);
		Task<Profil> UpdateProfilExpiryDate(Profil profil, string expiryDate);
		Task<Profil> UpdateProfilStatus(Profil profil, int statut);
		Task<Profil> GetUserProfil(Utilisateur user);
	}
}
