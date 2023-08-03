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

		public async Task<StatutDossierClient?> GetById(int id)
		{
			return await _unitOfWork.StatutDossierClients.GetById(id);
		}

		public async Task<StatutDossierClient> Update(StatutDossierClient statutUpdated, StatutDossierClient statut)
		{
			statut = statutUpdated;
			await _unitOfWork.CommitAsync();

			return statut;
		}

		public async Task<StatutDossierClient> Upgrade(StatutDossierClient statut) //Cette fonction sert à passer à l'étape suivante dans
																				  //le traitement d'un dossier crédit
		{
			var x = statut.EtapeDeroulementId + 1;
			var etapeDeroulement = await _unitOfWork.Etapes.GetByIdAsync(x);
			if (etapeDeroulement is null)
				throw new Exception("Mauvaise configuration des étapes correspondant à un type de prêt");
			statut.EtapeDeroulementId = x;
			statut.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			await _unitOfWork.CommitAsync();

			return statut;
		}

		public async Task<StatutDossierClient> Downgrade(StatutDossierClient statut, string motif) //Cette fonction sert à rejeter un dossier à l'étape précédente dans
																				   //le traitement d'un dossier crédit
		{
			statut.Motif = motif;
			statut.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			await _unitOfWork.CommitAsync();

			return statut;
		}


		public async Task<StatutDossierClient> Assign(StatutDossierClient statut)
		{
			statut.DecisionFinale = true;
			statut.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			await _unitOfWork.CommitAsync();

			return statut;
		}

		public async Task<StatutDossierClient> Reject(StatutDossierClient statut, string motif)
		{
			statut.DecisionFinale = false;
			statut.Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			statut.Motif = motif;
			await _unitOfWork.CommitAsync();

			return statut;
		}

	}
}
