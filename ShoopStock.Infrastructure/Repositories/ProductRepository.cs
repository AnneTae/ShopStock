using ShoopStock.Domain.Entites;
using ShoopStock.Infrastructure.Repositories.Base;

namespace ShoopStock.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
