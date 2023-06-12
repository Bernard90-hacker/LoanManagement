using LoanManagement.core;
using LoanManagement.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LoanManagement.Data.SqlServer
{
	public class UnitOfWork : IUnitOfWork
	{
		private UserRepository _userRepository;
		private LoanManagementDbContext _context;
		public UnitOfWork(LoanManagementDbContext context) => _context = context;

		public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

		public IUserTokenRepository UserTokens => throw new NotImplementedException();

		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync(IDbContextTransaction transaction)
		{
			await transaction.CommitAsync();
		}

		public async Task RollbackAsync(IDbContextTransaction transaction)
		{
			await transaction.RollbackAsync();
		}

		public async Task CreateSavepointAsync(IDbContextTransaction transaction, string savePointName)
		{
			await transaction.CreateSavepointAsync(savePointName);
		}

		public async Task RollbackSavepointAsync(IDbContextTransaction transaction, string savePointName)
		{
			await transaction.RollbackToSavepointAsync(savePointName);
		}

		public async Task ReleaseSavepointAsync(IDbContextTransaction transaction, string savePointName)
		{
			await transaction.ReleaseSavepointAsync(savePointName);
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
