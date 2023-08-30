using blog.Areas.Admin.Models;
using blog.Extension;
using blog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace blog.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogDbContext _context;

        public UserController(BlogDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        [HttpGet]
        public IActionResult Login()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            return View();
        }

        public IActionResult Register()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }
              
            return View();
        }
        [HttpPost]


        public async Task<IActionResult> Register(Account addUser)
        {
            if (string.IsNullOrEmpty(addUser.Email) || string.IsNullOrEmpty(addUser.Password) || string.IsNullOrEmpty(addUser.FullName))
            {
                TempData["ErrorMessage"] = "Vui lòng điền đầy đủ thông tin Email, Password và FullName.";
                return RedirectToAction("Register");
            }

            try
            {
                string hashedPassword = HashPasswordMD5.ToMD5(addUser.Password);

                var user = new Account()
                {
                    AccountId = addUser.AccountId,
                    FullName = addUser.FullName,
                    Email = addUser.Email,
                    Phone = addUser.Phone,
                    Password = hashedPassword,
                    //Salt = addUser.Salt,
                    Active = addUser.Active,
                    CreatedDate = DateTime.Now,
                    LastLogin = addUser.LastLogin,
                    RoleId = addUser.RoleId,
                    Posts = new List<Post>(), // Khởi tạo danh sách rỗng
                    Role = addUser.Role,
                    ProfileImage = addUser.ProfileImage
                };

                await _context.Accounts.AddAsync(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tạo tài khoản thành công!";
                return RedirectToAction("Register");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình tạo tài khoản.";
                // Log the exception or handle it appropriately
                return RedirectToAction("Register");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(Account model)
        {
            try
            {
                // Kiểm tra xem thông tin đăng nhập có chính xác không
                var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == HashPasswordMD5.ToMD5(model.Password));

                if (user != null)
                {
                    HttpContext.Session.SetString("AccountId", user.AccountId.ToString());
                    //HttpContext.Session.SetString("FullName", user.Email);

                    if (user.RoleId == 1) // Kiểm tra nếu người dùng có vai trò là quản trị viên
                    {
                        HttpContext.Session.SetString("IsAdmin", "true");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Đăng nhập thành công!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Email hoặc mật khẩu không chính xác.";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình đăng nhập.";
                // Log the exception or handle it appropriately
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            }
            catch
            {
                return RedirectToAction("Login", "User");
            }
        }
        
    }
}
