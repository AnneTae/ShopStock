using ShoopStock.Core.Infrastructure.DependencyInjection.LifeTimes;
using ShoopStock.Core.Models.Dtos;

namespace ShoopStock.Services.Interfaces;

public interface ICategoryService 
{
    Task<List<CategoryInfo>> GetCatgeories();
    Task<CategoryInfo> GetCatgeoryById(int id);
    Task<bool> CreateCategory(string name);
    Task<bool> UpdateCategory(CategoryInfo model);
    Task<bool> DeleteCategory(int id);
    
}
