namespace LoanManagement.core.Services.Loan_Management
{
	public interface IEmployeurService
	{
		Task<IEnumerable<Employeur>> GetAll();
		Task<PagedList<Employeur>> GetAll(EmployeurParameters parameters);
		Task<Employeur?> GetById(int id);
		Task<Employeur> GetByPhoneNumber(string number);
		Task<Employeur?> GetByMailBox(string mailBox);
		Task<Employeur> Create(Employeur employeur);
		Task<Employeur> Update(Employeur empUpdated, Employeur emp);
		Task<PretAccord?> GetDossier(int employeurId);
		Task Delete(Employeur emp);
	}
}
