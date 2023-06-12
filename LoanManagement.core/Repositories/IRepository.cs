using System.Linq.Expressions;

namespace LoanManagement.core.Repositories
{
	public interface IRepository<TEntity>
		where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		ValueTask<TEntity> GetByIdAsync(int id);
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
		Task AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);
	}
}
