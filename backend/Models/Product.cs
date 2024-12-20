using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public int? DiscountId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<Image> Images { get; } = new List<Image>();

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
}
