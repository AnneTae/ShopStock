using ShoopStock.Domain.Entites;
using ShoopStock.Infrastructure.Repositories.Base;

namespace ShoopStock.Infrastructure.Repositories;

public class CategoryRepositoriy : BaseRepository<Category>, ICategoryRepositoriy
{
    public CategoryRepositoriy(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
