using System.Linq.Expressions;

namespace ShoopStock.Infrastructure.Repositories.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? condition = null);

    Task<TEntity?> GetByIdAsync(int id);

    Task<TEntity> CreateAsync(TEntity item);

    TEntity Update(TEntity item);

    bool Delete(TEntity item);

    Task<int> CountAsync();
}