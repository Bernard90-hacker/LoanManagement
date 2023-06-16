namespace LoanManagement.core.Services.Users_Management
{
	public interface IEmployeService
	{
		Task<IEnumerable<Employe>> GetAll();
		Task<PagedList<Employe>> GetAll(EmployeParameters parameters);
		Task<Employe> GetEmployeByEmail(string email);
		Task<Employe> GetEmployeByDepartment(string codeDepartment);
		Task<Employe> GetEmployeByUsername(string username);
		Task<Employe> GetEmployeById(int id);
		Task<Employe> Create(Employe employe);
		Task<Employe> Update(Employe employe, Employe employeToBeUpdated);
		Task Delete(Employe employe);
	}
}
