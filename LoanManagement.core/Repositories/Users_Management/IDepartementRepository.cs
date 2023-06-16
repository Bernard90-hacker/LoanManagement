namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IDepartementRepository : IRepository<Departement>
	{
		Task<PagedList<Departement>> GetAll(DepartmentParameters parameters);
		Task<IEnumerable<Departement>> GetAll();
		Task<Departement> GetDepartmentById(int id);
		Task<Departement> GetDepartmentByCode(string code);
	}
}
