using System;
using System.Collections.Generic;

namespace blog.Models;

public partial class Category
{
    public int CategoryId { get; set; } // ID của danh mục

    public string? CategoryName { get; set; } // Tên danh mục

    public string? Title { get; set; } // Tiêu đề của danh mục

    public string? Alias { get; set; } // Phiên bản rút gọn và thân thiện hơn của tiêu đề danh mục, thường được sử dụng trong URL

    public string? MetaDesc { get; set; } // Mô tả meta của danh mục

    public string? MetaKey { get; set; } // Từ khóa meta của danh mục

    public string? Thumb { get; set; } // Đường dẫn tới hình ảnh đại diện (thumbnail) của danh mục

    public string? Icon { get; set; } // Đường dẫn tới biểu tượng (icon) của danh mục

    public string? Cover { get; set; } // Đường dẫn tới hình ảnh bìa (cover) của danh mục

    public string? Description { get; set; } // Mô tả chi tiết về danh mục

    public bool Published { get; set; } // Cờ xác định xem danh mục đã được xuất bản hay chưa

    public int? Ordering { get; set; } // Thứ tự sắp xếp của danh mục

    public int? Parents { get; set; } // ID của danh mục cha (nếu có)

    public int? Levels { get; set; } // Số cấp độ của danh mục

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>(); // Danh sách các bài viết liên quan đến danh mục
}
