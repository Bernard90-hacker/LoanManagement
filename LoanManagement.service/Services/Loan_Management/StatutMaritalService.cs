namespace LoanManagement.service.Services.Loan_Management
{
	public class StatutMaritalService : IStatutMaritalService
	{
		private IUnitOfWork _unitOfWork;
        public StatutMaritalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<StatutMarital> Create(StatutMarital statutMarital)
		{
			await _unitOfWork.StatutMaritals.AddAsync(statutMarital);
			await _unitOfWork.CommitAsync();

			return statutMarital;
		}

		public async Task Delete(StatutMarital statut)
		{
			_unitOfWork.StatutMaritals.Remove(statut);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<StatutMarital>> GetAll()
		{
			return await _unitOfWork.StatutMaritals.GetAllAsync();
		}

		public async Task<StatutMarital> GetById(int id)
		{
			return await _unitOfWork.StatutMaritals.GetById(id);
		}

		public async Task<StatutMarital> Update(StatutMarital updated, StatutMarital statutMarital)
		{
			statutMarital = updated;
			await _unitOfWork.CommitAsync();

			return statutMarital;
		}
	}
}
