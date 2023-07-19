namespace LoanManagement.service.Services.Loan_Management
{
	public class EmployeurService : IEmployeurService
	{
		private IUnitOfWork _unitOfWork;
        public EmployeurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<Employeur> Create(Employeur employeur)
		{
			await _unitOfWork.Employeurs.AddAsync(employeur);
			await _unitOfWork.CommitAsync();

			return employeur;
		}

		public async Task Delete(Employeur emp)
		{
			_unitOfWork.Employeurs.Remove(emp);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<Employeur>> GetAll()
		{
			return await _unitOfWork.Employeurs.GetAll();
		}

		public async Task<PagedList<Employeur>> GetAll(EmployeurParameters parameters)
		{
			return await _unitOfWork.Employeurs.GetAll(parameters);
		}

		public async Task<Employeur?> GetById(int id)
		{
			return await _unitOfWork.Employeurs.GetById(id);
		}

		public async Task<Employeur?> GetByMailBox(string mailBox)
		{
			return await _unitOfWork.Employeurs.GetByMailBox(mailBox);
		}

		public async Task<Employeur> GetByPhoneNumber(string number)
		{
			return await _unitOfWork.Employeurs.GetByPhoneNumber(number);
		}

		public async Task<PretAccord?> GetDossier(int employeurId)
		{
			var employeur = await _unitOfWork.Employeurs.GetById(employeurId);
			var dossiers = await _unitOfWork.PretAccords.GetAll();
			if (employeur is null) throw new Exception("Employeur inexistant");
			var result = (from x in dossiers
						 where x.EmployeurId == employeurId
						 select x).AsQueryable().FirstOrDefault();

			return result;
		}

		public async Task<Employeur> Update(Employeur empUpdated, Employeur emp)
		{
			emp = empUpdated;
			await _unitOfWork.CommitAsync();

			return emp;
		}
	}
}
