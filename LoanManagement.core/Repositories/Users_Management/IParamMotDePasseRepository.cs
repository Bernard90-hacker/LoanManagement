namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IParamMotDePasseRepository : IRepository<ParamMotDePasse>
	{
		Task<PagedList<ParamMotDePasse>> GetAll(ParamMotDePasseParameters parameters);
		Task<IEnumerable<ParamMotDePasse>> GetAll();
		Task<ParamMotDePasse?> GetById(int id);
		Task<ParamMotDePasse?> GetCurrentParameter();
	}
}
