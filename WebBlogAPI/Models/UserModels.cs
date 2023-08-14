using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebBlogAPI.Models
{
    public class UserModels
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        public string Confirmpass { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string Avatar { get; set; } = "https://ik.imagekit.io/alejk5lwty/image/30080a8689198c9c6b1c33591434995e.jpg?updatedAt=1678593087143";
        [NotMapped]
        public object Profile { get; internal set; }
    }
}