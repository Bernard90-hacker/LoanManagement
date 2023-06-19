using Constants.Pagination;
using LoanManagement.core.Models.Users_Management;
using LoanManagement.core.Pagination;

namespace LoanManagement.service.Services.Users_Management
{
	public class TypeJournalService : ITypeJournalService
	{
		private IUnitOfWork _unitOfWork;
        public TypeJournalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<TypeJournal> Create(TypeJournal type)
		{
			await _unitOfWork.TypeJournaux.AddAsync(type);
			await _unitOfWork.CommitAsync();

			return type;
		}

		public async Task Delete(TypeJournal typeJournal)
		{
			_unitOfWork.TypeJournaux.Remove(typeJournal);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<TypeJournal>> GetAll(TypeJournalParameters parameters)
		{
			return await _unitOfWork.TypeJournaux.GetAll(parameters);
		}

		public async Task<IEnumerable<TypeJournal>> GetAll()
		{
			return await _unitOfWork.TypeJournaux.GetAll();
		}

		public async Task<TypeJournal?> GetTypeJournalByCode(string code)
		{
			return await _unitOfWork.TypeJournaux.GetTypeJournalByCode(code);
		}

		public async Task<TypeJournal?> GetTypeJournalById(int id)
		{
			return await _unitOfWork.TypeJournaux.GetTypeJournalById(id);
		}

		public async Task<TypeJournal> Update(TypeJournal type, TypeJournal typeToBeUpdated)
		{
			typeToBeUpdated = type;
			await _unitOfWork.CommitAsync();

			return typeToBeUpdated;
		}
	}
}
