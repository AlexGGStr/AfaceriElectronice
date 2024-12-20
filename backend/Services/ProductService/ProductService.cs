using System.Security.Claims;
using AutoMapper;
using backend.DTOs.Product;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.ProductService;

public class ProductService : IProductService
{
    private readonly AlexContext _alexContext;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ProductService(AlexContext alexContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _alexContext = alexContext;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
    {
        var response = new ServiceResponse<GetProductDto>();
        var request = _httpContextAccessor.HttpContext.Request;
        var product = await _alexContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            response.Success = false;
            response.Message = "Product not found";
            return response;
        }
        var data = _mapper.Map<GetProductDto>(product);
        
        
        foreach (var image in data.Images)
        {
            image.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, image.ImageName);
        }
        response.Data = data;
        return response;
    }

    public async Task<ServiceResponse<List<GetProductDto>>> GetProductsByCategory(int categoryId, int page, int pageSize)
    {
        var response = new ServiceResponse<List<GetProductDto>>();
        var products = await _alexContext.Products.
            Where(p => p.CategoryId == categoryId || categoryId == 0).Include(p => p.Images)
            .ToListAsync();
        
        var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize)
            .ToList();
        

        response.Message = ((int)Math.Ceiling((double)products.Count / pageSize)).ToString();
        
        var request = _httpContextAccessor.HttpContext.Request;
        var data = _mapper.Map<List<GetProductDto>>(pagedProducts);
        foreach (var productDto in data)
        {
            foreach (var image in productDto.Images)
            {
                image.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, image.ImageName);
            }
        }
        
        response.Data = data;
        return response;
    }
    
    //add product 
    
}