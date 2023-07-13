namespace LoanManagement.service.Services.Loan_Management
{
	public class PeriodicitePaiementService : IPeriodicitePaiementService
	{
		private IUnitOfWork _unitOfWork;
        public PeriodicitePaiementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<PeriodicitePaiement> Create(PeriodicitePaiement periodicite)
		{
			await _unitOfWork.PeriodicitePaiements.AddAsync(periodicite);
			await _unitOfWork.CommitAsync();

			return periodicite;
		}

		public async Task Delete(PeriodicitePaiement p)
		{
			_unitOfWork.PeriodicitePaiements.Remove(p);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<PeriodicitePaiement>> GetAll()
		{
			return await _unitOfWork.PeriodicitePaiements.GetAllAsync();
		}

		public async Task<PeriodicitePaiement> GetById(int id)
		{
			return await _unitOfWork.PeriodicitePaiements.GetById(id);
		}

		public async Task<PeriodicitePaiement> Update(PeriodicitePaiement pUpDated, PeriodicitePaiement p)
		{
			p = pUpDated;
			await _unitOfWork.CommitAsync();

			return p;
		}
	}
}
