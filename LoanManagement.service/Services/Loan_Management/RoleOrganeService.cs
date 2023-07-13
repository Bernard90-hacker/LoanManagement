namespace LoanManagement.service.Services.Loan_Management
{
	public class RoleOrganeService : IRoleOrganeService
	{
        private IUnitOfWork _unitOfWork;
        public RoleOrganeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<RoleOrgane> Create(RoleOrgane role)
		{
			await _unitOfWork.Roles.AddAsync(role);
			await _unitOfWork.CommitAsync();

			return role;
		}

		public async Task Delete(RoleOrgane role)
		{
			_unitOfWork.Roles.Remove(role);
			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<RoleOrgane>> GetAll()
		{
			return await _unitOfWork.Roles.GetAll();
		}

		public async Task<RoleOrgane> GetById(int id)
		{
			return await _unitOfWork.Roles.GetById(id);
		}

		public async Task<RoleOrgane> Update(RoleOrgane roleUpdated, RoleOrgane role)
		{
			role = roleUpdated;
			await _unitOfWork.CommitAsync();

			return role;
		}
	}
}
