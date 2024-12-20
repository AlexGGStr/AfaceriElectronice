using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Role { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public string? VerificationToken { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? ResetTokenExpires { get; set; }

    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<UserAdress> UserAdresses { get; } = new List<UserAdress>();

    public virtual ICollection<UserPayment> UserPayments { get; } = new List<UserPayment>();
}
