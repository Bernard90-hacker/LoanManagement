namespace LoanManagement.service.Services.Loan_Management
{
	public class PretAccordService : IPretAccordService
	{
		private IUnitOfWork _unitOfWork;
        public PretAccordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<PretAccord> Create(PretAccord pret)
		{
			await _unitOfWork.PretAccords.AddAsync(pret);
			await _unitOfWork.CommitAsync();

			return pret;
		}

		public async Task Delete(PretAccord p)
		{
			_unitOfWork.PretAccords.Remove(p);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<PretAccord>> GetAll()
		{
			return await _unitOfWork.PretAccords.GetAll();
		}

		public async Task<PretAccord> GetById(int id)
		{
			return await _unitOfWork.PretAccords.GetById(id);
		}

		public async Task<PretAccord> GetPretAccordForDossier(int dossierId)
		{
			return await _unitOfWork.PretAccords.GetPretAccordForDossier(dossierId);
		}

		public async Task<PretAccord> Update(PretAccord pUpdated, PretAccord p)
		{
			p = pUpdated;
			await _unitOfWork.CommitAsync();

			return p;
		}
	}
}
