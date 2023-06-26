namespace LoanManagement.core.Services.Users_Management
{
	public interface IMenuService
	{
		Task<PagedList<Menu>> GetAll(MenuParameters parameters);
		Task<IEnumerable<Menu>> GetAll();
		Task<Menu?> GetMenuById(int id);
		Task<Menu?> GetMenuByCode(string code);
		Task<Menu?> GetMenuByPosition(int position);
		Task<IEnumerable<Menu>> GetSousMenus(int MenuId);
		Task<Menu> Create(Menu menu);
		Task<Menu> Update(Menu menu, Menu menuToBeUpdated);
		Task<Menu> UpdateMenuStatut(Menu menu, int statut);
		Task Delete(Menu menu);
	}
}
