using ShoopStock.Core.Infrastructure.DependencyInjection.LifeTimes;
using ShoopStock.Domain.Entites;
using ShoopStock.Infrastructure.Repositories.Base;

namespace ShoopStock.Infrastructure.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
}
