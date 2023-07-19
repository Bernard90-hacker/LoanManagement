namespace LoanManagement.service.Services.Loan_Management
{
	public class TypePretService : ITypePretService
	{
		private IUnitOfWork _unitOfWork;
        public TypePretService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<TypePret> Create(TypePret typePret)
		{
			await _unitOfWork.TypePrets.AddAsync(typePret);
			await _unitOfWork.CommitAsync();

			return typePret;
		}

		public async Task Delete(TypePret typePret)
		{
			_unitOfWork.TypePrets.Remove(typePret);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<TypePret>> GetAll()
		{
			return await _unitOfWork.TypePrets.GetAllAsync();
		}

		public async Task<TypePret?> GetById(int id)
		{
			return await _unitOfWork.TypePrets.GetById(id);
		}

		public async Task<IEnumerable<Deroulement>> GetDeroulements(int typePretId)
		{
			return await _unitOfWork.TypePrets.GetDeroulements(typePretId);
		}

		public async Task<TypePret> Update(TypePret typePretUpdated, TypePret typePret)
		{
			typePret = typePretUpdated;
			await _unitOfWork.CommitAsync();

			return typePret;
		}
	}
}
