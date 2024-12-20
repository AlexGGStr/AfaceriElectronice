using backend.DTOs.Image;
using backend.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace backend.DTOs.Product;

public class GetProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    
    public virtual Discount? Discount { get; set; }

    public int Quantity { get; set; } = 0;
    
    public virtual ICollection<GetImageDto> Images { get; set; } = new List<GetImageDto>();



    //public virtual ICollection<ProductInventory> ProductInventories { get; } = new List<ProductInventory>();

}