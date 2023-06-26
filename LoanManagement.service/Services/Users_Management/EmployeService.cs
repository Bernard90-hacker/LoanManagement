using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class EmployeService : IEmployeService
	{
        private readonly IUnitOfWork _unitOfWork;
        public EmployeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Employe> Create(Employe emp)
		{
			await _unitOfWork.Employes.AddAsync(emp);
			await _unitOfWork.CommitAsync();

			return emp;
		}

		public async Task Delete(Employe emp)
		{
			_unitOfWork.Employes.Remove(emp);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Employe>> GetAll(EmployeParameters parameters)
		{
			return await _unitOfWork.Employes.GetAll(parameters);
		}

		public async Task<IEnumerable<Employe>> GetAll()
		{
			return await _unitOfWork.Employes.GetAll();
		}

		public async Task<PagedList<Employe>?> GetEmployesByDepartment(string codeDepartment)
		{
			var departement = await _unitOfWork.Departements.GetDepartmentByCode(codeDepartment);
			if (departement is not null)
			{
				var employes = _unitOfWork.Employes.Find(x => x.DepartementId == departement.Id)
					.AsQueryable();
				var pageNumber = new EmployeParameters().PageNumber;
				var pageSize = new EmployeParameters().PageSize;

				return PagedList<Employe>.ToPagedList(employes, pageNumber, pageSize);
			}

			return null;
		}

		public async Task<Employe?> GetEmployeByEmail(string email)
		{
			return await _unitOfWork.Employes.GetEmployeByEmail(email);
		}

		public async Task<Employe?> GetEmployeById(int id)
		{
			return await _unitOfWork.Employes.GetEmployeById(id);
		}

		public async Task<Employe?> GetEmployeByUsername(string username)
		{
			var user = await _unitOfWork.Utilisateurs.GetUserByUsername(username);
			if (user is not null)
				return _unitOfWork.Employes.Find(x => x.UserId.Equals(user.Id)).FirstOrDefault();

			return null;
		}

		public async Task<Employe?> GetEmployeUserAccount(int userId)
		{
			var employes = await _unitOfWork.Employes.GetAll();

			return employes.Where(x => x.UserId == userId).FirstOrDefault();
		}

		public async Task<Employe> UpdateEmployePhoto(Employe emp, string photo)
		{
			emp.Photo = photo;
			emp.DateModification = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.CommitAsync();

			return emp;
		}

		public async Task<Employe> UpdateEmployeDepartment(Employe emp, int departementId)
		{
			emp.DepartementId = departementId;
			emp.DateModification = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.CommitAsync();

			return emp;
		}

		public async Task<Employe> Update(Employe emp, Employe empToBeUpdated)
		{
			empToBeUpdated = emp;
			emp.DateModification = DateTime.Now.ToString("dd/MM/yyyy");
			await _unitOfWork.CommitAsync();

			return empToBeUpdated;
		}

		public async Task<Employe?> GetEmployeeByFullName(string FullName)
		{
			var fullName = FullName.Split(" ");
			var lastName = string.Join(" ", fullName[1..]);
			var employee = await _unitOfWork.Employes.GetAll();
			var result = employee.Where(x => x.Nom == fullName[0] && x.Prenoms == string.Join(" ", fullName[1..])).FirstOrDefault();
		
			if (result is null) return null;

			return result;

		}
	}
}
