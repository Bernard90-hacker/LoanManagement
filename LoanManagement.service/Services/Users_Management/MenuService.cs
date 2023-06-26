using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class MenuService : IMenuService
	{
		private IUnitOfWork _unitOfWork;
        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Menu> Create(Menu menu)
		{
			await _unitOfWork.Menus.AddAsync(menu);
			await _unitOfWork.CommitAsync();

			return menu;
		}

		public async Task Delete(Menu menu)
		{
			_unitOfWork.Menus.Remove(menu);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Menu>> GetAll(MenuParameters parameters)
		{
			return await _unitOfWork.Menus.GetAll(parameters);
		}

		public async Task<IEnumerable<Menu>> GetAll()
		{
			return await _unitOfWork.Menus.GetAll();
		}

		public async Task<Menu?> GetMenuByCode(string code)
		{
			return await _unitOfWork.Menus.GetMenuByCode(code);
		}

		public async Task<Menu?> GetMenuById(int id)
		{
			return await _unitOfWork.Menus.GetMenuById(id);
		}

		public async Task<Menu?> GetMenuByPosition(int position)
		{
			return await _unitOfWork.Menus.GetMenuByPosition(position);
		}

		public async Task<Menu> Update(Menu menu, Menu menuToBeUpdated)
		{
			menuToBeUpdated = menu;
			await _unitOfWork.CommitAsync();

			return menuToBeUpdated;

		}

		public async Task<IEnumerable<Menu>> GetSousMenus(int MenuId)
		{
			var menus = await _unitOfWork.Menus.GetAll();

			return menus.Where(x => x.MenuId == MenuId).ToList();
		}

		public async Task<Menu> UpdateMenuStatut(Menu menu, int statut)
		{
			menu.Statut = statut;
			await _unitOfWork.CommitAsync();

			return menu;
		}
	}
}
