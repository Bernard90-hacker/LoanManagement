namespace LoanManagement.core.Services.Users_Management
{
	public interface IDepartmentService
	{
		Task<PagedList<Departement>> GetAll(DepartmentParameters parameters);
		Task<IEnumerable<Departement>> GetAll();
		Task<Departement> GetDepartmentById(int id);
		Task<Departement> GetDepartmentByCode(string code);
		Task<Departement> Create(Departement departement);
		Task<Departement> Update(Departement departement, Departement departementToBeUpdated);
		Task Delete(Departement departement);
	}
}
