using Microsoft.AspNetCore.Mvc;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BlogDbContext _context;

        public ProfileController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Profile()
        {
            string accountId = HttpContext.Session.GetString("AccountId");

            // Kiểm tra nếu accountId không null hoặc trống
            if (!string.IsNullOrEmpty(accountId))
            {
                // Thực hiện truy vấn để lấy thông tin tài khoản dựa trên accountId
                // Ví dụ:
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                // Kiểm tra nếu không tìm thấy thông tin tài khoản
                if (account == null)
                {
                    // Chuyển hướng đến trang đăng nhập
                    return RedirectToAction("Login", "User");
                }
                // Trả về view profile và truyền thông tin tài khoản vào view
                return View(account);
            }
            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login", "User");
        }
    }
}
