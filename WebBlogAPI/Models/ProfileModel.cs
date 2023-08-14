using System;

namespace WebBlogAPI.Models
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Address { get; set; } = "Lost Angel";
        public string City { get; set; } = "Tokyo";
        public string Phone { get; set; } = "(097) 234-5678";
        public string Avatar { get; set; } = "https://ik.imagekit.io/alejk5lwty/image/30080a8689198c9c6b1c33591434995e.jpg?updatedAt=1678593087143";
        public string Email { get; set; } = "authorautofix@gmail.com";
        public string FullName { get; set; } = "Monkey Monkey";
        public string Education { get; set; } = "";
        public string Description { get; set; } = "";
    }
}