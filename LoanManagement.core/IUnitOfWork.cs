
namespace LoanManagement.core
{
	public interface IUnitOfWork : IDisposable
	{

		IUserRepository Users { get; }
		IUserTokenRepository UserTokens { get; }

		Task<int> CommitAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitAsync(IDbContextTransaction transaction);
		Task RollbackAsync(IDbContextTransaction transaction);
		Task CreateSavepointAsync(IDbContextTransaction transaction, string savePointName);
		Task RollbackSavepointAsync(IDbContextTransaction transaction, string savePointName);
		Task ReleaseSavepointAsync(IDbContextTransaction transaction, string savePointName);
	}
}
