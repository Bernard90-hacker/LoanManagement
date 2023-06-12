using System.Linq.Expressions;

namespace LoanManagement.Data.SqlServer
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly DbContext _context;
        public Repository(DbContext context)
        {
			_context = context;
        }
        public async Task AddAsync(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await _context.Set<TEntity>().AddRangeAsync(entities);
			await _context.SaveChangesAsync();
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{

			return _context.Set<TEntity>()
				.Where(predicate)
				.ToList();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>()
			   .ToListAsync();
		}

		public ValueTask<TEntity> GetByIdAsync(int id)
		{
			return	_context.Set<TEntity>()
			   .FindAsync(id);
		}

		public void Remove(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
			_context.SaveChanges();
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			_context.Set<TEntity>()
				.RemoveRange(entities);
		}

		public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return _context.Set<TEntity>()
				 .SingleOrDefaultAsync(predicate);
		}
	}
}
