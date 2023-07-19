namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IParamMotDePasseRepository : IRepository<ParamGlobal>
	{
		Task<PagedList<ParamGlobal>> GetAll(ParamMotDePasseParameters parameters);
		Task<IEnumerable<ParamGlobal>> GetAll();
		Task<ParamGlobal?> GetById(int id);
		Task<ParamGlobal?> GetCurrentParameter();
	}
}
