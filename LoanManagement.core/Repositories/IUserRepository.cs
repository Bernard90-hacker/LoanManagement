namespace LoanManagement.core.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<PagedList<User>> GetAll(UserParameters parameters);
		Task<IEnumerable<User>> GetAll();
		Task<bool> GetUser(string email);
		Task<User?> GetUser(int Id);
		Task<User?> Login(LoginDto dto);	
	}
}
