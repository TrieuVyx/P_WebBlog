using System;
using System.Collections.Generic;

namespace blog.Models;

public partial class Post
{
    public int PostId { get; set; } // ID của bài viết

    public string? Title { get; set; } // Tiêu đề của bài viết

    public string? ShortContents { get; set; } // Nội dung ngắn gọn của bài viết

    public string? Contents { get; set; } // Nội dung đầy đủ và chi tiết của bài viết

    public string? Thumb { get; set; } // Đường dẫn tới hình ảnh đại diện (thumbnail) của bài viết

    public string? Alias { get; set; } // Phiên bản rút gọn và thân thiện hơn của tiêu đề bài viết, thường được sử dụng trong URL

    public string? Author { get; set; } // Tên tác giả của bài viết

    public string? Tags { get; set; } // Các từ khóa hoặc nhãn (labels) được gán cho bài viết để phân loại hoặc tìm kiếm

    public int? CategoryId { get; set; } // ID của danh mục (category) mà bài viết thuộc về

    public bool Published { get; set; } // Cờ xác định xem bài viết đã được xuất bản hay chưa

    public bool IsHot { get; set; } // Cờ xác định xem bài viết có được đánh dấu là nổi bật (hot) hay không

    public int? AccountId { get; set; } // ID của tài khoản (account) liên quan đến bài viết

    public bool IsNewFeed { get; set; } // Cờ xác định xem bài viết có được coi là tin tức mới (new feed) hay không

    public DateTime? CreatedDate { get; set; } // Ngày và giờ tạo bài viết

    public int Views { get; set; } // Số lượt xem của bài viết

    public virtual Account? Account { get; set; } // Tài khoản (account) liên quan đến bài viết

    public virtual Category? Category { get; set; } // Danh mục (category) mà bài viết thuộc về
}
