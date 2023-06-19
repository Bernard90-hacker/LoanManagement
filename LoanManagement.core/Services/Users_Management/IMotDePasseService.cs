namespace LoanManagement.core.Services.Users_Management
{
	public interface IMotDePasseService
	{
		Task<PagedList<MotDePasse>> GetAll(MotDePasseParameters parameters);
		Task<IEnumerable<MotDePasse>> GetAll();
		Task<IEnumerable<MotDePasse>?> GetPasswordsByHash(string hash);
		Task<MotDePasse?> GetPasswordById(int id);
	}
}
