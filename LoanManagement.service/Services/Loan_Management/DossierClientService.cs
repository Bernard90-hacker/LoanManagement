namespace LoanManagement.service.Services.Loan_Management
{
	public class DossierClientService : IDossierClientService
	{
		private IUnitOfWork _unitOfWork;

        public DossierClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<DossierClient> Create(DossierClient dossier)
		{
			await _unitOfWork.DossierClients.AddAsync(dossier);
			await _unitOfWork.CommitAsync();

			return dossier;
		}

		public async Task Delete(DossierClient dossierClient)
		{
			_unitOfWork.DossierClients.Remove(dossierClient);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<DossierClient>> GetAll()
		{
			return await _unitOfWork.DossierClients.GetAll();
		}

		public async Task<PagedList<DossierClient>> GetAll(DossierClientParameters parameters)
		{
			return await _unitOfWork.DossierClients.GetAll(parameters);
		}

		public async Task<DossierClient?> GetById(int Id)
		{
			return await _unitOfWork.DossierClients.GetById(Id);
		}

		public async Task<DossierClient?> GetByNumber(string numeroDossier)
		{
			return await _unitOfWork.DossierClients.GetByNumber(numeroDossier);
		}

		public async Task<Deroulement?> GetDossierDeroulement(int typePretId, double montant)
		{
			return await _unitOfWork.DossierClients.GetDossierDeroulement(typePretId, montant);
		}

		public async Task<Employeur?> GetEmployeurByDossier(int id)
		{
			return await _unitOfWork.DossierClients.GetEmployeurByDossier(id);
		}

		public async Task<IEnumerable<InfoSanteClient>> GetInfoSanteByDossier(int dossierId)
		{
			return await _unitOfWork.DossierClients.
				GetInfoSanteByDossier(dossierId);
		}

		public async Task<StatutDossierClient?> GetStatut(int id)
		{
			return await _unitOfWork.DossierClients.GetStatut(id);
		}

		public async Task<DossierClient> Update(DossierClient dossierClientUpdated, DossierClient dossierClient)
		{
			dossierClient = dossierClientUpdated;
			await _unitOfWork.CommitAsync();

			return dossierClient;
		}

		public async Task Cloturer(DossierClient dossierClient)
		{
			dossierClient.Cloturer = true;
			dossierClient.DateCloture = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
		
			await _unitOfWork.CommitAsync();

		}

		public async Task AddCouverture(DossierClient dossierClient, string couverture)
		{
			dossierClient.CouvertureEmprunteur = couverture;
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<DossierClient>> GetClosed()
		{
			return await _unitOfWork.DossierClients
				.GetClosed();
		}
		
	}
}
