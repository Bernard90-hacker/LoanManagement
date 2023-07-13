namespace LoanManagement.service.Services.Loan_Management
{
	public class DeroulementService : IDeroulementService
	{
		private IUnitOfWork _unitOfWork;
		public DeroulementService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Deroulement> Create(Deroulement deroulement)
		{
			await _unitOfWork.Deroulements.AddAsync(deroulement);
			await _unitOfWork.CommitAsync();

			return deroulement;
		}

		public async Task Delete(Deroulement deroulement)
		{
			_unitOfWork.Deroulements.Remove(deroulement);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<Deroulement>> GetAll()
		{
			return await _unitOfWork.Deroulements.GetAll();
		}

		public async Task<Deroulement?> GetById(int id)
		{
			return await _unitOfWork.Deroulements.GetById(id);
		}

		public async Task<IEnumerable<EtapeDeroulement>> GetSteps(int deroulementId)
		{
			return await _unitOfWork.Deroulements.GetSteps(deroulementId);
		}

		public async Task<Deroulement> Update(Deroulement deroulement, Deroulement deroulementUpdated)
		{
			deroulement.Plancher = deroulementUpdated.Plancher;
			deroulement.Plafond = deroulementUpdated.Plafond;
			deroulement.NiveauInstance = deroulement.NiveauInstance;
			await _unitOfWork.CommitAsync();

			return deroulement;
		}
	}
}
