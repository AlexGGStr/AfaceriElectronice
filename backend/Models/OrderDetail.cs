using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? PaymentId { get; set; }

    public int? AdressId { get; set; }

    public int? Total { get; set; }

    public virtual OrderAdress? Adress { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderPayment? Payment { get; set; }

    public virtual User User { get; set; } = null!;
}
