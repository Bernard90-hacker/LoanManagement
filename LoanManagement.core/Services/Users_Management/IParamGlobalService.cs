namespace LoanManagement.core.Services.Users_Management
{
	public interface IParamGlobalService
	{
		Task<PagedList<ParamGlobal>> GetAll(ParamMotDePasseParameters parameters);
		Task<IEnumerable<ParamGlobal>> GetAll();
		Task<ParamGlobal?> GetById(int id);
		Task<ParamGlobal> Create(ParamGlobal p);
		Task<ParamGlobal> Update(ParamGlobal p, ParamGlobal paramToBeUpdated);
		Task Delete(ParamGlobal p);
		Task<ParamGlobal> UpdatePasswordsExpiryFrequency(ParamGlobal param, int ExpiryDate);
		Task<ParamGlobal> PasswordMustIncludeUpperCase(ParamGlobal param, bool response);
		Task<ParamGlobal> PasswordMustIncludeDigits(ParamGlobal param, bool response);
		Task<ParamGlobal> PasswordMustExcludeUsername(ParamGlobal param, bool response);
		Task<ParamGlobal> PasswordLength(ParamGlobal param, int taille);
		Task<ParamGlobal?> GetCurrentParameter();
	}
}
