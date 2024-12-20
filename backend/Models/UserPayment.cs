using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class UserPayment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? PaymentType { get; set; }

    public string? AccountNo { get; set; }

    public virtual User? User { get; set; }
}
