using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class ParamGlobalService : IParamGlobalService
	{
		private IUnitOfWork _unitOfWork;
        public ParamGlobalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<ParamGlobal> Create(ParamGlobal p)
		{
			await _unitOfWork.ParamMotDePasses.AddAsync(p);
			await _unitOfWork.CommitAsync();

			return p;
		}

		public async Task Delete(ParamGlobal p)
		{
			_unitOfWork.ParamMotDePasses.Remove(p);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<ParamGlobal>> GetAll(ParamMotDePasseParameters parameters)
		{
			return await _unitOfWork.ParamMotDePasses.GetAll(parameters);
		}

		public async Task<IEnumerable<ParamGlobal>> GetAll()
		{
			return await _unitOfWork.ParamMotDePasses.GetAll();
		}

		public async Task<ParamGlobal?> GetById(int id)
		{
			return await _unitOfWork.ParamMotDePasses.GetById(id);
		}

		public async Task<ParamGlobal> Update(ParamGlobal p, ParamGlobal paramToBeUpdated)
		{
			paramToBeUpdated.SmtpName = p.SmtpName;
			paramToBeUpdated.SmtpEmail = p.SmtpEmail;
			paramToBeUpdated.SmtpClient = p.SmtpClient;
			paramToBeUpdated.Port = p.Port;
			paramToBeUpdated.FromPassword = p.FromPassword;
			await _unitOfWork.CommitAsync();

			return paramToBeUpdated;
		}

		public async Task<ParamGlobal> UpdatePasswordsExpiryFrequency(ParamGlobal param, int ExpiryFrequency)
		{
			param.DelaiExpiration = ExpiryFrequency;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamGlobal> PasswordMustIncludeSpecialCharacters(ParamGlobal param, bool response)
		{
			param.IncludeSpecialCharacters = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamGlobal> PasswordMustIncludeDigits(ParamGlobal param, bool response)
		{
			param.IncludeDigits = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamGlobal> PasswordMustIncludeUpperCase(ParamGlobal param, bool response)
		{
			param.IncludeUpperCase = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

        public async Task<ParamGlobal> PasswordMustIncludeLowerCase(ParamGlobal param, bool response)
        {
            param.IncludeLowerCase = response;
            await _unitOfWork.CommitAsync();

            return param;
        }

        public async Task<ParamGlobal> PasswordMustExcludeUsername(ParamGlobal param, bool response)
		{
			param.ExcludeUsername = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamGlobal> PasswordLength(ParamGlobal param, int taille)
		{
			param.Taille = taille;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamGlobal?> GetCurrentParameter()
		{
			var param = await _unitOfWork.ParamMotDePasses.GetAll();
			if (param is null) return null;

			return param.FirstOrDefault();
		}

        public async Task<ParamGlobal> UpdatePwdParam(ParamGlobal p, ParamGlobal paramToBeUpdated)
        {
            paramToBeUpdated.IncludeDigits = p.IncludeDigits;
            paramToBeUpdated.IncludeLowerCase = p.IncludeLowerCase;
            paramToBeUpdated.IncludeUpperCase = p.IncludeUpperCase;
            paramToBeUpdated.IncludeSpecialCharacters = p.IncludeSpecialCharacters;
            paramToBeUpdated.ExcludeUsername = p.ExcludeUsername;
            paramToBeUpdated.DelaiExpiration = p.DelaiExpiration;
            paramToBeUpdated.Taille = p.Taille;
            await _unitOfWork.CommitAsync();

            return paramToBeUpdated;
        }
    }
}
