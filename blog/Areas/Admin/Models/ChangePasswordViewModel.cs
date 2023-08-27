﻿using System.ComponentModel.DataAnnotations;

namespace blog.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name = "Mật khẩu hiện tại")]
        public string? PasswordNow { get; set; }
        [Display(Name ="Mật khẩu mới")]
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [MinLength(5,ErrorMessage ="Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        public string? Password { get; set;}
        [MinLength(5,ErrorMessage ="Bạn cần đặt mật khẩu tối thiểu 5 ký tự")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password",ErrorMessage ="Mặt khẩu không giống nhau")]
        public string? ConfirmPassword { get; set;}
    }
}
