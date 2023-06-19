namespace LoanManagement.core.Services.Users_Management
{
	public interface IDirectionService 
	{
		Task<PagedList<Direction>> GetAll(DirectionParameters parameters);
		Task<IEnumerable<Direction>> GetAll();
		Task<PagedList<Departement>?> GetAllDepartementsByDirection(string code);
		Task<Direction?> GetDirectionById(int id);
		Task<Direction?> GetDirectionByCode(string code);
		Task<Direction> Create(Direction direction);
		Task<Direction> Update(Direction direction, Direction directionToBeUpdated);
		Task Delete(Direction direction);

	}
}
