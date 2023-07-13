namespace LoanManagement.core.Repositories.Loan_Management
{
	public interface ITypePretRepository : IRepository<TypePret>
	{
		Task<IEnumerable<TypePret>> GetAll();
		Task<TypePret?> GetById(int id);
		Task<IEnumerable<Deroulement>> GetDeroulements(int typePretId);
	}
}
