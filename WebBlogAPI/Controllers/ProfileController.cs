using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WebBlogAPI.Data;
using WebBlogAPI.Models;

namespace WebBlogAPI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly mvcContextDb _mvcContextDb;

        public ProfileController(mvcContextDb mvcContextDb)
        {
            _mvcContextDb = mvcContextDb;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            string username = HttpContext.Session.GetString("Username");
            string avatar = HttpContext.Session.GetString("Avatar");
            string Idstring = HttpContext.Session.GetString("Id");

            if (!string.IsNullOrEmpty(Idstring))
            {
                try
                {
                    Guid id = Guid.Parse(Idstring);
                    // In giá trị GUID ra màn hình
                    // Hoặc ghi log: logger.LogDebug(id.ToString());
                    ProfileModel model = new ProfileModel
                    {
                        Username = username,
                        Avatar = avatar,
                        Id = id
                    };
                    return View(model);
                }
                catch (FormatException ex)
                {
                    // Xử lý khi không thể chuyển đổi giá trị thành Guid
                    // Ví dụ: Ghi log lỗi và chuyển hướng đến trang đăng nhập hoặc trang khác
                    // logger.LogError(ex, "Không thể chuyển đổi giá trị thành Guid");
                    return RedirectToAction("Profile", "Profile");
                }
            }

            // Xử lý khi không có giá trị Id trong session
            // Ví dụ: Chuyển hướng đến trang đăng nhập hoặc trang khác
            return RedirectToAction("Profile", "Profile");
        }

        public async Task<IActionResult> ViewProfile()
        {
            string idString = HttpContext.Session.GetString("Id");

            if (string.IsNullOrEmpty(idString))
            {
                // Xử lý khi không có Id trong session
                return RedirectToAction("Profile", "Profile");
            }

            return View();
        }
    }
}