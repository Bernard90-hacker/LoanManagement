namespace LoanManagement.core.Services.Users_Management
{
	public interface IHabilitationProfilService
	{
		Task<PagedList<HabilitationProfil>> GetAll(HabilitationProfilParameters parameters);
		Task<IEnumerable<HabilitationProfil>> GetAll();
		Task<HabilitationProfil> GetHabilitationProfilById(int id);
		Task<HabilitationProfil> Create(HabilitationProfil profil);
		Task<HabilitationProfil> Update(HabilitationProfil profil, 
			HabilitationProfil habilitationProfilToBeUpdated);
		Task Delete(HabilitationProfil profil);
	}
}
