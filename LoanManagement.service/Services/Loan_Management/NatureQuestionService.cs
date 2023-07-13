namespace LoanManagement.service.Services.Loan_Management
{
	public class NatureQuestionService : INatureQuestionService
	{
        private IUnitOfWork _unitOfWork;
        public NatureQuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<NatureQuestion> Create(NatureQuestion nature)
		{
			await _unitOfWork.Natures.AddAsync(nature);
			await _unitOfWork.CommitAsync();

			return nature;
		}

		public async Task Delete(NatureQuestion nature)
		{
			_unitOfWork.Natures.Remove(nature);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<NatureQuestion>> GetAll()
		{
			return await _unitOfWork.Natures.GetAll();
		}

		public async Task<NatureQuestion> GetById(int id)
		{
			return await _unitOfWork.Natures.GetById(id);
		}

		public async Task<NatureQuestion> Update(NatureQuestion natureUpdated, NatureQuestion nature)
		{
			nature = natureUpdated;
			await _unitOfWork.CommitAsync();

			return nature;
		}
	}
}
