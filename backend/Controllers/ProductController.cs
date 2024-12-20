using backend.DTOs.Product;
using backend.Models;
using backend.Services;
using backend.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    //GET product by id
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetProductDto>>> GetProductById(int id)
    {
        return Ok(await _productService.GetProductById(id));
    }
    
    //GET products by category
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetProductsByCategory(int categoryId, int page, int pageSize)
    {
        return Ok(await _productService.GetProductsByCategory(categoryId, page, pageSize));
    }
}