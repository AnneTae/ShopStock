using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoopStock.Core.Exceptions;
using ShoopStock.Core.Models.Dtos;
using ShoopStock.Domain.Entites;
using ShoopStock.Infrastructure;
using ShoopStock.Services.BaseServices;
using ShoopStock.Services.Interfaces;

namespace ShoopStock.Services.Services;

public class CategoryService : BaseService, ICategoryService
{
    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<bool> CreateCategory(string name)
    {
        var productType = new Category
        {
            Name = name
        };
        await UnitOfWork.CategoryRepositoriy.CreateAsync(productType);
        var result = await UnitOfWork.Commit();

        return result.Succeeded;

    }

    public async Task<bool> DeleteCategory(int id)
    {
        var product = await UnitOfWork.CategoryRepositoriy.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Category Not Found");
        UnitOfWork.CategoryRepositoriy.Delete(product);
        var result = await UnitOfWork.Commit();
        return result.Succeeded;
    }

    public async Task<CategoryInfo> GetCatgeoryById(int id)
    {
        var category = await UnitOfWork.CategoryRepositoriy.GetByIdAsync(id)
            ?? throw new EntityNotFoundException("Category Not Found");
        return Mapper.Map<Category, CategoryInfo>(category);
    }

    public async Task<List<CategoryInfo>> GetCatgeories()
    {
        var categories = await UnitOfWork.CategoryRepositoriy.Get().ToListAsync();
        return Mapper.Map<List<Category>, List<CategoryInfo>>(categories);
    }

    public async Task<bool> UpdateCategory(CategoryInfo model)
    {
        var productType = await UnitOfWork.CategoryRepositoriy.GetByIdAsync(model.Id)
             ?? throw new EntityNotFoundException("Category Not Found");
        productType.Name = model.Name;
        UnitOfWork.CategoryRepositoriy.Update(productType);
        var result = await UnitOfWork.Commit();
        return result.Succeeded;
    }
}
