using AutoMapper;
using ShoopStock.Core.Models.Dtos;
using ShoopStock.Core.Models.Requests;
using ShoopStock.Core.Models.Response;
using ShoopStock.Domain.Entites;

namespace ShoopStock.Services.Mappering;

public class AutoMapperProfileConfiguration : Profile
{
    public AutoMapperProfileConfiguration()
    {
        CreateMap<Product, CategoryInfo>().ReverseMap();
        CreateMap<Category, CategoryInfo>().ReverseMap();

        CreateMap<CategoryInfo, CategoryResponse>();
        CreateMap<ProductInfo, ProductResponse>();
        CreateMap<ProductInfo, Product>().ReverseMap();


        CreateMap<ProductRequest, ProductInfo>();
        CreateMap<CategoryRequest, CategoryInfo>();

    }
}
