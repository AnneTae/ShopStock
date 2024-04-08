using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShoopStock.Infrastructure.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? condition)
    {
        IQueryable<TEntity> set = _dbContext.Set<TEntity>();

        if (condition is not null)
        {
            set = set.Where(condition);
        }

        return set;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = new object[] { id };

        return await _dbContext.Set<TEntity>().FindAsync(entity);
    }

    public async Task<TEntity> CreateAsync(TEntity item)
    {
        var entityEntry = await _dbContext.AddAsync(item);

        return entityEntry.Entity;
    }

    public TEntity Update(TEntity item)
    {
        var entityEntry = _dbContext.Set<TEntity>().Update(item);

        return entityEntry.Entity;
    }

    public virtual Task<int> CountAsync()
    {
        return _dbContext.Set<TEntity>().CountAsync();
    }

    public bool Delete(TEntity item)
    {
        return _dbContext.Set<TEntity>().Remove(item).IsKeySet;
    }
}