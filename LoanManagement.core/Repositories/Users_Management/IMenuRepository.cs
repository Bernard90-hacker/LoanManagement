namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IMenuRepository : IRepository<Menu>
	{
		Task<PagedList<Menu>> GetAll(MenuParameters parameters);
		Task<IEnumerable<Menu>> GetAll();
		Task<Menu?> GetMenuById(int id);
		Task<Menu?> GetMenuByCode(string code);
		Task<Menu?> GetMenuByPosition(int position);
	}
}
