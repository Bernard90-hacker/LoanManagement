namespace LoanManagement.service.Services.Loan_Management
{
	public class InfoSanteClientService : ISanteClientService
	{
		private IUnitOfWork _unitOfWork;
        public InfoSanteClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<InfoSanteClient> Create(InfoSanteClient info)
		{
			await _unitOfWork.InfoSantes.AddAsync(info);
			await _unitOfWork.CommitAsync();

			return info;
		}

		public async Task Delete(InfoSanteClient info)
		{
			_unitOfWork.InfoSantes.Remove(info);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<InfoSanteClient>> GetAll()
		{
			return await _unitOfWork.InfoSantes.GetAllAsync();
		}

		public async Task<InfoSanteClient?> GetAnswerForQuestion(int natureQuestionId)
		{
			return await _unitOfWork.InfoSantes.
				GetAnswerForQuestion(natureQuestionId);
		}

		public async Task<InfoSanteClient> GetById(int id)
		{
			return await _unitOfWork.InfoSantes
				.GetById(id);
		}

		public async Task<InfoSanteClient> Update(InfoSanteClient infoUpdated, InfoSanteClient info)
		{
			info = infoUpdated;
			await _unitOfWork.CommitAsync();

			return info;
		}
	}
}
