namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IDirectionRepository : IRepository<Direction>
	{
		Task<PagedList<Direction>> GetAll(DirectionParameters parameters);
		Task<IEnumerable<Direction>> GetAll();
		Task<Direction?> GetDirectionById(int id);
		Task<Direction?> GetDirectionByCode(string code);
	}
}
