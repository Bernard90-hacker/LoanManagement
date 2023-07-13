namespace LoanManagement.service.Services.Loan_Management
{
	public class EtapeDeroulementService : IEtapeDeroulementService
	{
		private IUnitOfWork _unitOfWork;
        public EtapeDeroulementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<EtapeDeroulement> Create(EtapeDeroulement etape)
		{
			await _unitOfWork.Etapes.AddAsync(etape);
			await _unitOfWork.CommitAsync();

			return etape;
		}

		public async Task Delete(EtapeDeroulement etape)
		{
			_unitOfWork.Etapes.Remove(etape);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<EtapeDeroulement>> GetAll()
		{
			return await _unitOfWork.Etapes.GetAllAsync();
		}

		public async Task<EtapeDeroulement> GetById(int Id)
		{
			return await _unitOfWork.Etapes.GetById(Id);
		}

		public async Task<EtapeDeroulement> Update(EtapeDeroulement etapeUpdated, EtapeDeroulement etape)
		{
			etape = etapeUpdated;
			await _unitOfWork.CommitAsync();

			return etape;
		}
	}
}
