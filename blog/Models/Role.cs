﻿using System;
using System.Collections.Generic;

namespace blog.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; } = "USER";

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}