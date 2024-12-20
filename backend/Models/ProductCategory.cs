using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Descriprion { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
