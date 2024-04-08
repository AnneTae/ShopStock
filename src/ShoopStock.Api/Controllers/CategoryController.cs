using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoopStock.Core.Models.Dtos;
using ShoopStock.Core.Models.Requests;
using ShoopStock.Core.Models.Response;
using ShoopStock.Services.Interfaces;

namespace ShoopStock.Api.Controllers;

[Route("api/categories")]
public class CategoryController : BaseApiController
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public CategoryController(ICategoryService categoryService, IProductService productService, IMapper mapper)
    {
        _categoryService = categoryService;
        _productService = productService;
        _mapper = mapper;
    }

    // GET: api/<ProductTypeController>
    [HttpGet]
    public async Task<List<CategoryResponse>> Get()
    {
        var productTypes = await _categoryService.GetCatgeories();
        return _mapper.Map<List<CategoryInfo>, List<CategoryResponse>>(productTypes);
    }

    // GET api/<ProductTypeController>/5
    [HttpGet("{id}")]
    public async Task<CategoryResponse> Get(int id)
    {
        var productType = await _categoryService.GetCatgeoryById(id);
        return _mapper.Map<CategoryInfo, CategoryResponse>(productType);
    }

    // POST api/<ProductTypeController>
    [HttpPost]
    public async Task<bool> Post(CategoryRequest request)
    {

        return await _categoryService.CreateCategory(request.Name);

    }

    // PUT api/<ProductTypeController>/5
    [HttpPut("{id}")]
    public async Task<bool> Put([FromRoute] int id, [FromBody] CategoryRequest request)
    {

       
            var model = _mapper.Map<CategoryRequest, CategoryInfo>(request);
            model.Id = id;
            return await _categoryService.UpdateCategory(model);
     

    }

    // DELETE api/<ProductTypeController>/5
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
            return await _categoryService.DeleteCategory(id);

    }
}
