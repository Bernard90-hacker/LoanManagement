namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface IEtapeDeroulementRepository : IRepository<EtapeDeroulement>
	{
		Task<IEnumerable<EtapeDeroulement>> GetAll();
		Task<EtapeDeroulement?> GetById(int Id);
	}
}
