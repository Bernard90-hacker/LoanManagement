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

		public async Task<IEnumerable<Utilisateur>> GetUsersByMembreOrgane(int membreOrganeId)
		{
			return await _unitOfWork.MembreOrganes.GetUsersByMembreOrgane(membreOrganeId);
		}

		public async Task<IEnumerable<Utilisateur>> GetUsersByOrganeDecision(int organeDecisionId)
		{
			return await _unitOfWork.MembreOrganes.GetUsersByOrganeDecision(organeDecisionId);
		}

		public async Task<MembreOrgane> Update(MembreOrgane membreUpdated, MembreOrgane membre)
		{
			membre = membreUpdated;
			await _unitOfWork.CommitAsync();

			return membre;
		}
	}
}
