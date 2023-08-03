using LoanManagement.core.Models.Users_Management;

namespace LoanManagement.service.Services.Loan_Management
{
	public class MembreOrganeService : IMembreOrganeService
	{
		private IUnitOfWork _unitOfWork;
        public MembreOrganeService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }
        public async Task<MembreOrgane> Create(MembreOrgane membre)
		{
			await _unitOfWork.MembreOrganes.AddAsync(membre);
			await _unitOfWork.CommitAsync();

			return membre;
		}

		public async Task Delete(MembreOrgane membre)
		{
			_unitOfWork.MembreOrganes.Remove(membre);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<MembreOrgane>> GetAll()
		{
			return await _unitOfWork.MembreOrganes.GetAll();
		}

		public async Task<PagedList<MembreOrgane>> GetAll(MembreOrganeParameters parameters)
		{
			return await _unitOfWork.MembreOrganes.GetAll(parameters);
		}

		public async Task<MembreOrgane?> GetById(int Id)
		{
			return await _unitOfWork.MembreOrganes.GetByIdAsync(Id);
		}

		public async Task<IEnumerable<Utilisateur>> GetUsersByMembreOrgane(int membreOrganeId)
		{
			return await _unitOfWork.MembreOrganes.GetUsersByMembreOrgane(membreOrganeId);
		}

		public async Task<IEnumerable<Utilisateur>> GetUsersByOrganeDecision(int organeDecisionId)
		{
			return await _unitOfWork.MembreOrganes.GetUsersByOrganeDecision(organeDecisionId);
		}
		public async Task<string?> GetMembreUsername(int memberId)
		{
			var membre = await _unitOfWork.MembreOrganes.GetByIdAsync(memberId);
			var result = (from x in (await _unitOfWork.Utilisateurs.GetAllAsync())
						 where membre.UtilisateurId == x.Id
						 select x.Username).FirstOrDefault();

			return result;
		}
		public async Task<MembreOrgane> Update(MembreOrgane membreUpdated, MembreOrgane membre)
		{
			membre = membreUpdated;
			await _unitOfWork.CommitAsync();

			return membre;
		}

        public async Task<EtapeDeroulement?> GetEtapeByUser(int userId)
		{ 
			var membre = (from x in (await _unitOfWork.MembreOrganes.GetAllAsync())
						  where x.UtilisateurId == userId
						  select x).FirstOrDefault();
			if (membre is null) throw new Exception("L'utilisateur n'appartient à aucun organe de décision");
			
			var etape = (from x in (await _unitOfWork.Etapes.GetAllAsync())
						 where x.MembreOrganeId == membre.Id
						 select x).FirstOrDefault();

			return etape;	
        }
        public async Task<MembreOrgane?> GetMembreByStep(int etapeId)
		{
			var etape = await _unitOfWork.Etapes.GetByIdAsync(etapeId);
			if (etape is null) throw new Exception("Etape inexistant.");
			var result = (from x in await _unitOfWork.MembreOrganes.GetAllAsync()
						  where etape.MembreOrganeId == x.Id
						  select x).FirstOrDefault();

			return result;
		}
    }
}
