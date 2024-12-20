﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class UserAdress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string AdressLine { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? PostalCode { get; set; }

    public string Telephone { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
