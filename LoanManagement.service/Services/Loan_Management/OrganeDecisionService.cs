namespace LoanManagement.service.Services.Loan_Management
{
	public class OrganeDecisionService : IOrganeDecisionService
	{
		private IUnitOfWork _unitOfWork;
        public OrganeDecisionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<OrganeDecision> Create(OrganeDecision organe)
		{
			await _unitOfWork.OrganeDecisions.AddAsync(organe);
			await _unitOfWork.CommitAsync();

			return organe;
		}

		public async Task Delete(OrganeDecision organe)
		{
			_unitOfWork.OrganeDecisions.Remove(organe);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<OrganeDecision>> GetAll()
		{
			return await _unitOfWork.OrganeDecisions.GetAllAsync();
		}

		public async Task<OrganeDecision> GetById(int id)
		{
			return await _unitOfWork.OrganeDecisions.GetById(id);
		}

		public async Task<OrganeDecision> Update(OrganeDecision organe, OrganeDecision organeUpdated)
		{
			organe = organeUpdated;
			await _unitOfWork.CommitAsync();

			return organe;
		}
	}
}
