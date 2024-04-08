using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoopStock.Core.Models.Dtos;
using ShoopStock.Core.Models.Requests;
using ShoopStock.Core.Models.Response;
using ShoopStock.Services.Interfaces;

namespace ShoopStock.Api.Controllers;

[Route("api/products")]
public class ProductController : BaseApiController
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<List<ProductResponse>> Get()
    {
        var products = await _productService.GetProducts();
        return _mapper.Map<List<ProductInfo>, List<ProductResponse>>(products);
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<ProductResponse> Get(int id)
    {
        var product = await _productService.GetProductById(id);
        return _mapper.Map<ProductInfo, ProductResponse>(product);
    }

    // POST api/<ProductController>
    [HttpPost]
    public async Task<bool> Post(ProductRequest request)
    {
      
            var hasProduct = await _productService.CheckExistsProductByCode(request.Code);
            if (!hasProduct)
            {
                var model = _mapper.Map<ProductRequest, ProductInfo>(request);
                return await _productService.CretaeProduct(model);
            }

            throw new Exception(" product with this code already exists");


    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public async Task<bool> Put([FromRoute] int id, [FromBody] ProductRequest request)
    {
        var model = _mapper.Map<ProductRequest, ProductInfo>(request);
        return await _productService.UpdateProduct(id, model);
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _productService.DeleteProduct(id);
    }
}
