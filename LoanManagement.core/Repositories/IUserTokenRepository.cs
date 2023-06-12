	namespace LoanManagement.core.Repositories
{
	public interface IUserTokenRepository : IRepository<UserToken>
	{
		Task<PagedList<UserToken>> GetAll(UserTokenParameters parameters);
		Task<IEnumerable<UserToken>> GetAll();

	}
}
