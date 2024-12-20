namespace backend.Services.ImageService;

public interface IImageService
{
    Task<ServiceResponse<int>> AddImage(IFormFile image, int productId);
    Task<ServiceResponse<int>> DeleteImage(int id);
    Task<ServiceResponse<List<string>>> GetImages(int id, int productId);
}