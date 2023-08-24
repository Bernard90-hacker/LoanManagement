namespace LoanManagement.core.Repositories.Users_Management
{
	public interface IEmployeRepository : IRepository<Employe>
	{
		Task<IEnumerable<Employe>> GetAll();
		Task<PagedList<Employe>> GetAll(EmployeParameters parameters);
		Task<Employe?> GetEmployeByEmail(string email);
		Task<Employe?> GetEmployeById(int id);
		Task<Employe?> GetEmployeByMatricule(string matricule);
	}
}
