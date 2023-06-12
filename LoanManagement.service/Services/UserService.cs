using Constants.Pagination;
using LoanManagement.core;
using LoanManagement.core.Dto;
using LoanManagement.core.Models;
using LoanManagement.core.Pagination;
using LoanManagement.core.Services;

namespace LoanManagement.service.Services
{
	public class UserService : IUserService
	{
		private IUnitOfWork _unitOfWork;
		public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

		public async Task<User> Create(User user)
		{
			await _unitOfWork.Users.AddAsync(user);
			await _unitOfWork.CommitAsync();

			return user;
		}

		public async Task Delete(User user)
		{
			_unitOfWork.Users.Remove(user);
			await _unitOfWork.CommitAsync();
		}

		public async Task<bool> Get(string email)
		{
			return await _unitOfWork.Users.GetUser(email);
		}

		public async Task<User?> Get(int Id)
		{
			return await _unitOfWork.Users.GetUser(Id);
		}

		public async Task<PagedList<User>> GetAll(UserParameters parameters)
		{
			return await _unitOfWork.Users.GetAll(parameters);
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _unitOfWork.Users.GetAll();
		}

		public async Task<User?> Login(LoginDto dto)
		{
			return await _unitOfWork.Users.Login(dto);
		}

		public async Task Update(UserUpdatedDto dto, User user)
		{
			dto.Email = user.Email;
			dto.FirstName = user.FirstName;
			dto.LastName = user.LastName;
			await _unitOfWork.CommitAsync();
		}
	}
}
