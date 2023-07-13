namespace LoanManagement.core.Services.Loan_Management
{
	public interface ITypePretService
	{
		Task<IEnumerable<TypePret>> GetAll();
		Task<TypePret> GetById(int id);
		Task<IEnumerable<Deroulement>> GetDeroulements(int typePretId);
		Task<TypePret> Create(TypePret typePret);
		Task<TypePret> Update(TypePret typePretUpdated, TypePret typePret);
		Task Delete(TypePret typePret);
	}
}
