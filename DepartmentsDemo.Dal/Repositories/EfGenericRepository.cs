using System.Linq.Expressions;
using DepartmentsDemo.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DepartmentsDemo.Dal.Repositories;

public class EfGenericRepository<TDbContext, TEntity> : IEfGenericRepository<TEntity> 
	where TDbContext: DbContext 
	where TEntity : class
{
	private readonly TDbContext _context;
	private readonly DbSet<TEntity> _dbSet;
 
	public EfGenericRepository(TDbContext context)
	{
		_context = context;
		_dbSet = context.Set<TEntity>();
	}
	
	
	public async Task<IEnumerable<TEntity>> GetAsync()
	{
		return await _dbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
	}
         
	public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> match)
	{
		return await _dbSet.Where(match).AsNoTracking().ToListAsync().ConfigureAwait(false);
	}
	
	public ValueTask<TEntity?> GetByIdAsync<TId>(TId id)
	{
		return _dbSet.FindAsync(id);
	}
 
	
	public void Create(TEntity item)
	{
		_dbSet.Add(item);
	}
	public void Update(TEntity item)
	{
		//_context.Entry(item).State = EntityState.Modified;
		_context.Update(item);
	}
	public void Remove(TEntity item)
	{
		_dbSet.Remove(item);
	}

	public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync().ConfigureAwait(false);

	
		
	public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
	{
		return await Include(includeProperties).ToListAsync().ConfigureAwait(false);
	}
 
	
	public IEnumerable<TEntity> GetWithInclude(Func<TEntity,bool> predicate, 
		params Expression<Func<TEntity, object>>[] includeProperties)
	{
		var query =  Include(includeProperties);
		return query.Where(predicate).ToList();
	}
    
	private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
	{
		IQueryable<TEntity> query = _dbSet.AsNoTracking();
		return includeProperties
			.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
	}
	
}