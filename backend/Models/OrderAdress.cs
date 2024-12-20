using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class OrderAdress
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string? AdressLine { get; set; }

    public string PostalCode { get; set; } = null!;

    public string? Telephone { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
