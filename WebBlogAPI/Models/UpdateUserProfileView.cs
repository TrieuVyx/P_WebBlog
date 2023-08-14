using System;

namespace WebBlogAPI.Models
{
    public class UpdateUserProfileView
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Address { get; set; } = "Lost Angel";
        public string City { get; set; } = "Tokyo";
        public string Phone { get; set; } = "(097) 234-5678";
        public string Avatar { get; set; }
        public string Email { get; set; } = "authorautofix@gmail.com";
        public string FullName { get; set; } = "Monkey Monkey";
        public string Education { get; set; } = "";
        public string Description { get; set; } = "";
    }
}