using System.Linq.Expressions;

namespace DepartmentsDemo.Dal.Interfaces;

public interface IEfGenericRepository<TEntity> where TEntity : class
{
	Task<IEnumerable<TEntity>> GetAsync();
	Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> match);
	ValueTask<TEntity?> GetByIdAsync<TId>(TId id);
	Task<IEnumerable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties);
	
	void Create(TEntity item);
	void Update(TEntity item);
	void Remove(TEntity item);
	
	Task<int> SaveChangesAsync();
}