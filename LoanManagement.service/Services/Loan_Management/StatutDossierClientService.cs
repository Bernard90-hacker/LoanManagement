namespace LoanManagement.service.Services.Loan_Management
{
	public class StatutDossierClientService : IStatutDossierClientService
	{
		private IUnitOfWork _unitOfWork;
        public StatutDossierClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<StatutDossierClient> Create(StatutDossierClient statut)
		{
			await _unitOfWork.StatutDossierClients.AddAsync(statut);
			await _unitOfWork.CommitAsync();

			return statut;
		}

		public async Task Delete(StatutDossierClient statut)
		{
			_unitOfWork.StatutDossierClients.Remove(statut);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<StatutDossierClient>> GetAll()
		{
			return await _unitOfWork.StatutDossierClients.GetAllAsync();
		}

		public async Task<StatutDossierClient> GetById(int id)
		{
			return await _unitOfWork.StatutDossierClients.GetById(id);
		}

		public async Task<StatutDossierClient> Update(StatutDossierClient statutUpdated, StatutDossierClient statut)
		{
			statut = statutUpdated;
			await _unitOfWork.CommitAsync();

			return statut;
		}
	}
}
