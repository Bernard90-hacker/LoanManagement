namespace LoanManagement.service.Services.Loan_Management
{
	public class CompteService : ICompteService
	{
		private IUnitOfWork _unitOfWork;
        public CompteService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }
        public async Task<Compte> Create(Compte compte)
		{
			await _unitOfWork.Comptes.AddAsync(compte);
			await _unitOfWork.CommitAsync();

			return compte;
		}

		public async Task<Compte> DecreaseAmount(Compte compte, double solde)
		{
			compte.Solde -= solde;
			await _unitOfWork.CommitAsync();

			return compte;
		}

		public async Task Delete(Compte compte)
		{
			_unitOfWork.Comptes.Remove(compte);
			await _unitOfWork.CommitAsync();
		}

		public async Task<PagedList<Compte>> GetAll(CompteParameters parameters)
		{
			return await _unitOfWork.Comptes.GetAll(parameters);
		}

		public async Task<IEnumerable<Compte>> GetAll()
		{
			return await _unitOfWork.Comptes.GetAll();
		}

		public async Task<Compte?> GetByClient(int clientId)
		{
			return await _unitOfWork.Comptes.GetByClient(clientId);
		}

		public async Task<Compte?> GetById(int id)
		{
			return await _unitOfWork.Comptes.GetById(id);
		}

		public async Task<Compte?> GetByNumber(string accountNumber)
		{
			return await _unitOfWork.Comptes.GetByNumber(accountNumber);
		}

		public async Task<Compte> IncreaseAmount(Compte compte, double solde)
		{
			compte.Solde += solde;
			await _unitOfWork.CommitAsync();

			return compte;
		}
	}
}
