using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class DirectionService : IDirectionService
	{
        private readonly IUnitOfWork _unitOfWork;
        public DirectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Direction> Create(Direction direction)
		{
			await _unitOfWork.Directions.AddAsync(direction);
			await _unitOfWork.CommitAsync();

			return direction;
		}

		public async Task Delete(Direction direction)
		{
			_unitOfWork.Directions.Remove(direction);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Direction>> GetAll(DirectionParameters parameters)
		{
			return await _unitOfWork.Directions.GetAll(parameters);
		}

		public async Task<IEnumerable<Direction>> GetAll()
		{
			return await _unitOfWork.Directions.GetAll();
		}

		public async Task<Direction?> GetDirectionByCode(string code)
		{
			return await _unitOfWork.Directions.GetDirectionByCode(code);
		}

		public async Task<Direction?> GetDirectionById(int id)
		{
			return await _unitOfWork.Directions.GetDirectionById(id);
		}

		public async Task<Direction> Update(Direction direction, string libelle)
		{
			direction.Libelle = libelle;
			await _unitOfWork.CommitAsync();

			return direction;
		}

		public async Task<PagedList<Departement>?> GetAllDepartementsByDirection(string code)
		{
			var direction = await _unitOfWork.Directions.GetDirectionByCode(code);
			var pageNumber = new DepartmentParameters().PageNumber;
			var pageSize = new DepartmentParameters().PageSize;
			if (direction is not null)
			{
				var departements = (_unitOfWork.Departements.Find(x => x.DirectionId == direction.Id).ToList()).AsQueryable();
				return PagedList<Departement>.ToPagedList(departements, pageNumber, pageSize);
			}

			return null;
		}
	}
}
