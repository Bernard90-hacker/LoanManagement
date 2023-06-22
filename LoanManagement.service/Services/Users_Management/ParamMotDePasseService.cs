using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class ParamMotDePasseService : IParamMotDePasseService
	{
		private IUnitOfWork _unitOfWork;
        public ParamMotDePasseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<ParamMotDePasse> Create(ParamMotDePasse p)
		{
			await _unitOfWork.ParamMotDePasses.AddAsync(p);
			await _unitOfWork.CommitAsync();

			return p;
		}

		public async Task Delete(ParamMotDePasse p)
		{
			_unitOfWork.ParamMotDePasses.Remove(p);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<ParamMotDePasse>> GetAll(ParamMotDePasseParameters parameters)
		{
			return await _unitOfWork.ParamMotDePasses.GetAll(parameters);
		}

		public async Task<IEnumerable<ParamMotDePasse>> GetAll()
		{
			return await _unitOfWork.ParamMotDePasses.GetAll();
		}

		public async Task<ParamMotDePasse?> GetById(int id)
		{
			return await _unitOfWork.ParamMotDePasses.GetById(id);
		}

		public async Task<ParamMotDePasse> Update(ParamMotDePasse p, ParamMotDePasse paramToBeUpdated)
		{
			paramToBeUpdated = p;
			await _unitOfWork.CommitAsync();

			return paramToBeUpdated;
		}

		public async Task<ParamMotDePasse> UpdatePasswordsExpiryFrequency(ParamMotDePasse param, int ExpiryFrequency)
		{
			param.DelaiExpiration = ExpiryFrequency;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamMotDePasse> PasswordMustIncludeSpecialCharacters(ParamMotDePasse param, bool response)
		{
			param.IncludeSpecialCharacters = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamMotDePasse> PasswordMustIncludeDigits(ParamMotDePasse param, bool response)
		{
			param.IncludeDigits = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamMotDePasse> PasswordMustIncludeUpperCase(ParamMotDePasse param, bool response)
		{
			switch (response)
			{
				case true:

					param.IncludeUpperCase = true;
					param.IncludeLowerCase = false;
					break;

				case false:
					param.IncludeUpperCase = false;
					param.IncludeLowerCase = true;
					break;
			}
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamMotDePasse> PasswordMustExcludeUsername(ParamMotDePasse param, bool response)
		{
			param.ExcludeUsername = response;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamMotDePasse> PasswordLength(ParamMotDePasse param, int taille)
		{
			param.Taille = taille;
			await _unitOfWork.CommitAsync();

			return param;
		}

		public async Task<ParamMotDePasse?> GetCurrentParameter()
		{
			var param = await _unitOfWork.ParamMotDePasses.GetAll();
			if (param is null) return null;

			return param.FirstOrDefault();
		}
	}
}
