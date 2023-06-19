using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class DepartementService : IDepartmentService
	{
        private readonly IUnitOfWork _unitOfWork;
        public DepartementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Departement> Create(Departement app)
		{
			await _unitOfWork.Departements.AddAsync(app);
			await _unitOfWork.CommitAsync();

			return app;
		}

		public async Task Delete(Departement app)
		{
			_unitOfWork.Departements.Remove(app);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Departement>> GetAll(DepartmentParameters parameters)
		{
			return await _unitOfWork.Departements.GetAll(parameters);
		}

		public async Task<IEnumerable<Departement>> GetAll()
		{
			return await _unitOfWork.Departements.GetAll();
		}

		public async Task<Departement?> GetDepartementById(int id)
		{
			return await _unitOfWork.Departements.GetDepartmentById(id);
		}

		public async Task<Departement?> GetDepartmentByCode(string code)
		{
			return await _unitOfWork.Departements.GetDepartmentByCode(code);
		}

		public Task<Departement?> GetDepartmentById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<Departement> Update(Departement dept, Departement deptToBeUpdated)
		{
			deptToBeUpdated = dept;
			await _unitOfWork.CommitAsync();

			return deptToBeUpdated;
		}
	}
}
