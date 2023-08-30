using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace blog.Models;

public partial class Account
{
    public int AccountId { get; set; } // ID của tài khoản

    [Required(ErrorMessage = "FullName is required.")]
    public string? FullName { get; set; } // Họ và tên của tài khoản

    [Required(ErrorMessage = "Email is required.")]
    public string? Email { get; set; } // Địa chỉ email của tài khoản

    public string? Phone { get; set; } // Số điện thoại của tài khoản

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; } // Mật khẩu của tài khoản

    public string? Salt { get; set; } // Chuỗi salt (muối) được sử dụng cho mật khẩu

    public bool Active { get; set; } // Trạng thái hoạt động của tài khoản

    public DateTime? CreatedDate { get; set; } // Ngày và giờ tạo tài khoản

    public DateTime? LastLogin { get; set; } // Ngày và giờ đăng nhập gần nhất

    public string? Address { get; set; } = "Tokyo"; // Địa chỉ của tài khoản (mặc định là "Tokyo")

    public int? RoleId { get; set; } = 2; // ID của vai trò (role) của tài khoản (mặc định là 2)

    public string? ProfileImage { get; set; } = "https://ik.imagekit.io/alejk5lwty/image/e7f7e8b1f5c2b250dc742370e4dd55d5.jpg?updatedAt=1678593086858"; // Đường dẫn tới hình ảnh hồ sơ của tài khoản (mặc định)

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>(); // Danh sách các bài viết liên quan đến tài khoản

    public virtual Role? Role { get; set; } // Vai trò (role) của tài khoản 
}
