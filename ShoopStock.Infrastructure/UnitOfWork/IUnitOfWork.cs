using ShoopStock.Core.Infrastructure.DependencyInjection.LifeTimes;
using ShoopStock.Core.Models;
using ShoopStock.Infrastructure.Repositories;

namespace ShoopStock.Infrastructure;

public interface IUnitOfWork 
{
    public IProductRepository ProductRepository { get; }

    public ICategoryRepositoriy CategoryRepositoriy { get; }

    Task<UnitOfWorkExceptionModel> Commit();
}
