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
	}
}
