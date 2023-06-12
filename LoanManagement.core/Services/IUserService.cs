namespace LoanManagement.core.Services
{
	public interface IUserService
	{
		Task<PagedList<User>> GetAll(UserParameters parameters);
		Task<IEnumerable<User>> GetAll();
		Task<bool> Get(string email);
		Task<User?> Get(int Id);
		Task<User> Create(User user);
		Task<User?> Login(LoginDto dto);
		Task Update(UserUpdatedDto userToBeUpdated,  User user);
		Task Delete(User user);
	}
}
