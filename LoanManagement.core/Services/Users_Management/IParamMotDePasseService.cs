namespace LoanManagement.core.Services.Users_Management
{
	public interface IParamMotDePasseService
	{
		Task<PagedList<ParamMotDePasse>> GetAll(ParamMotDePasseParameters parameters);
		Task<IEnumerable<ParamMotDePasse>> GetAll();
		Task<ParamMotDePasse?> GetById(int id);
		Task<ParamMotDePasse> Create(ParamMotDePasse p);
		Task<ParamMotDePasse> Update(ParamMotDePasse p, ParamMotDePasse paramToBeUpdated);
		Task Delete(ParamMotDePasse p);
		Task<ParamMotDePasse> UpdatePasswordsExpiryFrequency(ParamMotDePasse param, int ExpiryDate);
		Task<ParamMotDePasse> PasswordMustIncludeUpperCase(ParamMotDePasse param, bool response);
		Task<ParamMotDePasse> PasswordMustIncludeDigits(ParamMotDePasse param, bool response);
		Task<ParamMotDePasse> PasswordMustExcludeUsername(ParamMotDePasse param, bool response);
		Task<ParamMotDePasse> PasswordLength(ParamMotDePasse param, int taille);
		Task<ParamMotDePasse?> GetCurrentParameter();
	}
}
