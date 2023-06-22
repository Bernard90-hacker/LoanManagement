namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IMotDePasseRepository : IRepository<MotDePasse>
	{
		Task<PagedList<MotDePasse>> GetAll(MotDePasseParameters parameters);
		Task<IEnumerable<MotDePasse>> GetAll();
		Task<MotDePasse?> GetPasswordByHash(string hash);
		Task<MotDePasse?> GetPasswordsById(int id);
	}
}
