using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoopStock.Core.Exceptions;
using ShoopStock.Core.Models.Dtos;
using ShoopStock.Domain.Entites;
using ShoopStock.Infrastructure;
using ShoopStock.Services.BaseServices;
using ShoopStock.Services.Interfaces;

namespace ShoopStock.Services.Services;

public class ProductService : BaseService, IProductService
{
    public ProductService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<bool> CretaeProduct(ProductInfo model)
    {
        var product = Mapper.Map<ProductInfo, Product>(model);
        product.CreateDate = DateTime.UtcNow;
        product.CategoryId = model.CategoryId;
        await UnitOfWork.ProductRepository.CreateAsync(product);
        var result = await UnitOfWork.Commit();
        return result.Succeeded;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await UnitOfWork.ProductRepository.GetByIdAsync(id)
             ?? throw new EntityNotFoundException("Product Not Found");

        UnitOfWork.ProductRepository.Delete(product);
        var result = await UnitOfWork.Commit();
        return result.Succeeded;
    }

    public async Task<ProductInfo> GetProductById(int id)
    {
        var product = await UnitOfWork.ProductRepository.GetByIdAsync(id)
             ?? throw new EntityNotFoundException("Category Not Found");

        return Mapper.Map<Product, ProductInfo>(product);
    }

    public async Task<List<ProductInfo>> GetProducts()
    {
        var products = await UnitOfWork.ProductRepository.Get().ToListAsync();
        return Mapper.Map<List<Product>, List<ProductInfo>>(products);
    }

    public async Task<bool> UpdateProduct(int id, ProductInfo model)
    {
        var product = await UnitOfWork.ProductRepository.GetByIdAsync(id)
              ?? throw new EntityNotFoundException("Category Not Found");

        product.Name = model.Name;
        product.CategoryId = model.CategoryId;
        product.CategoryId = model.CategoryId;
        product.Price = model.Price;
        product.Code = model.Code;
        product.UpdateDate = DateTime.UtcNow;
        UnitOfWork.ProductRepository.Update(product);

        var result = await UnitOfWork.Commit();
        return result.Succeeded;

    }

    public async Task<bool> CheckExistsProductByCode(string productCode)
    {
        return await UnitOfWork.ProductRepository.Get().AnyAsync(item => item.Code == productCode);
    }

}
