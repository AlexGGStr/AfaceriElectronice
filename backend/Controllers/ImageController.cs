using backend.Services;
using backend.Services.ImageService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : Controller
{
    private readonly IImageService _imageService;
    
    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    [HttpPost("{productId}")]
    public async Task<ActionResult<ServiceResponse<int>>> AddImage(IFormFile image, int productId)
    {
        return Ok(await _imageService.AddImage(image, productId));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteImage(int id)
    {
        return Ok(await _imageService.DeleteImage(id));
    }
}