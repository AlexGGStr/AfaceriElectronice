using backend.DTOs.Product;
using backend.Models;

namespace backend.Services.ProductService;

public interface IProductService
{
    Task<ServiceResponse<GetProductDto>> GetProductById(int id);
    Task<ServiceResponse<List<GetProductDto>> > GetProductsByCategory(int categoryId, int page, int pagesize);
}