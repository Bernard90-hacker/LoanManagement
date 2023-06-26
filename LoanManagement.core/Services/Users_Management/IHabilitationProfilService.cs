namespace LoanManagement.core.Services.Users_Management
{
	public interface IHabilitationProfilService
	{
		Task<PagedList<HabilitationProfil>> GetAll(HabilitationProfilParameters parameters);
		Task<IEnumerable<HabilitationProfil>> GetAll();
		Task<HabilitationProfil?> GetHabilitationProfilById(int id);
		Task<HabilitationProfil> Create(HabilitationProfil profil);
		Task<HabilitationProfil> Update(HabilitationProfil profil, 
			HabilitationProfil habilitationProfilToBeUpdated);
		Task<HabilitationProfil> UpdateInsertion(HabilitationProfil profil, bool response);
		Task<HabilitationProfil> UpdateModification(HabilitationProfil profil, bool response);
		Task<HabilitationProfil> UpdateSuppression(HabilitationProfil profil, bool response);
		Task<HabilitationProfil> UpdateEdition(HabilitationProfil profil, bool response);
		Task<HabilitationProfil> UpdateGeneration(HabilitationProfil profil, bool response);
		Task Delete(HabilitationProfil profil);
	}
}
