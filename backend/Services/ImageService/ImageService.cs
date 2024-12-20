using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.ImageService;

public class ImageService : IImageService
{
    private readonly AlexContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;
    
    public ImageService(AlexContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<ServiceResponse<int>> AddImage(IFormFile image, int productId)
    {
        await _context.Images.AddAsync(new Image
        {
            ImageName = await SaveFile(image),
            ProductId = productId
        });
        await _context.SaveChangesAsync();
        
        return new ServiceResponse<int>
        {
            Data = productId
        };
    }

    public async Task<ServiceResponse<int>> DeleteImage(int id)
    {
        var serviceResponse = new ServiceResponse<int>();
        var file = await _context.Images.FirstOrDefaultAsync(f => f.Id == id);
        if (file == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Not found";
            serviceResponse.Data = -1;
        } else
        {
            DeleteFile(file.ImageName);
            _context.Images.Remove(file);
            await _context.SaveChangesAsync();
            serviceResponse.Data = id;
        }
        return serviceResponse;
    }

    public Task<ServiceResponse<List<string>>> GetImages(int id, int productId)
    {
        throw new NotImplementedException();
    }
    
    private async Task<string> SaveFile(IFormFile formFile)
    {
        string fileName = new String(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(" ", "-");
        fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(formFile.FileName);
        var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(fileStream);
        }

        return fileName;
    }

    private void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", fileName);
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);
    }
}