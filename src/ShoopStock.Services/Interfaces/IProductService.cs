
using ShoopStock.Core.Infrastructure.DependencyInjection.LifeTimes;
using ShoopStock.Core.Models.Dtos;

namespace ShoopStock.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductInfo>> GetProducts();
    Task<ProductInfo> GetProductById(int id);
    Task<bool> CretaeProduct(ProductInfo model);
    Task<bool> UpdateProduct(int id, ProductInfo model);
    Task<bool> DeleteProduct(int id);
    Task<bool> CheckExistsProductByCode(string productCode);
    
}
