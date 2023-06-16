namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IHabilitationProfilRepository : IRepository<HabilitationProfil>
	{
		Task<PagedList<HabilitationProfil>> GetAll(HabilitationProfilParameters parameters);
		Task<IEnumerable<HabilitationProfil>> GetAll();
		Task<HabilitationProfil> GetHabilitationProfilById(int id);
	}
}
