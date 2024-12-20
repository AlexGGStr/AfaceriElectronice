using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class OrderPayment
{
    public int Id { get; set; }

    public string PaymentType { get; set; } = null!;

    public string AccountNo { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
