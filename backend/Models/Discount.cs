using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Discount
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int DiscountPercent { get; set; }

    public short Active { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
