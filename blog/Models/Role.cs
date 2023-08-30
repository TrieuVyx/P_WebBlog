using System;
using System.Collections.Generic;

namespace blog.Models;

public partial class Role
{
    public int RoleId { get; set; } // ID của vai trò (role)

    public string? RoleName { get; set; } // Tên của vai trò (role)

    public string? RoleDescription { get; set; } // Mô tả về vai trò (role)

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>(); // Danh sách các tài khoản liên quan đến vai trò

}
