﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual OrderDetail Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
