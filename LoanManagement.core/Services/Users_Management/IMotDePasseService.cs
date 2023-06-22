namespace LoanManagement.core.Services.Users_Management
{
	public interface IMotDePasseService
	{
		Task<PagedList<MotDePasse>> GetAll(MotDePasseParameters parameters);
		Task<IEnumerable<MotDePasse>> GetAll();
		Task<MotDePasse?> GetPasswordByHash(string hash);
		Task<MotDePasse?> GetPasswordById(int id);
		Task<MotDePasse> Create(MotDePasse motDePasse);
	}
}
