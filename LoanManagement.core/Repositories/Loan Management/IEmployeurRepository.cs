namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IEmployeurRepository : IRepository<Employeur>
	{
		Task<IEnumerable<Employeur>> GetAll();
		Task<PagedList<Employeur>> GetAll(EmployeurParameters parameters);
		Task<Employeur?> GetById(int id);
		Task<Employeur?> GetByPhoneNumber(string number);
		Task<Employeur?> GetByMailBox(string mailBox);
	}
}
