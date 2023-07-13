namespace LoanManagement.core.Services.Loan_Management
{
	public interface IEtapeDeroulementService
	{
		Task<IEnumerable<EtapeDeroulement>> GetAll();
		Task<EtapeDeroulement> GetById(int Id);
		Task<EtapeDeroulement> Create(EtapeDeroulement etape);
		Task<EtapeDeroulement> Update(EtapeDeroulement etapeUpdated, EtapeDeroulement etape);
		Task Delete(EtapeDeroulement etape);
	}
}
