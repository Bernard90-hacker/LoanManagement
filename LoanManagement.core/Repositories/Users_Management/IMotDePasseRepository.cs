namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IMotDePasseRepository : IRepository<MotDePasse>
	{
		Task<PagedList<MotDePasse>> GetAll(MotDePasseParameters parameters);
		Task<IEnumerable<MotDePasse>> GetAll();
		Task<IEnumerable<MotDePasse>> GetPasswordsByHash(string hash);
		Task<MotDePasse> GetPasswordsById(int id);
	}
}
