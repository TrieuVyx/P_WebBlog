using Microsoft.AspNetCore.Mvc;
using WebBlogAPI.Data;
using WebBlogAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace WebBlogAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly mvcContextDb _mvcContextDb;

        public UserController(mvcContextDb mvcContextDb)
        {
            _mvcContextDb = mvcContextDb;
        }

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
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View();
        }

        public IActionResult Logout()
        {
            // Perform logout logic here

            // Clear session data
            HttpContext.Session.Clear();

            // Redirect to a specific page after logout
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModels AddUser)
        {
            var user = new UserModels()
            {
                Id = Guid.NewGuid(),
                Username = AddUser.Username,
                Email = AddUser.Email,
                Password = AddUser.Password,
                Confirmpass = AddUser.Confirmpass
            };

            await _mvcContextDb.Users.AddAsync(user);
            await _mvcContextDb.SaveChangesAsync();

            TempData["SuccessMessage"] = "Tạo tài khoản thành công!";
            return RedirectToAction("Register");

        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModels AddUser)
        {
            bool isAuthenticated = await AuthenticateUserAsync(AddUser.Username, AddUser.Password);

            if (isAuthenticated)
            {
                Guid id = Guid.Empty; // Khởi tạo giá trị mặc định cho id

                var user = await _mvcContextDb.Users.FirstOrDefaultAsync(u => u.Username == AddUser.Username && u.Password == AddUser.Password);
                if (user != null && Guid.TryParse(user.Id.ToString(), out Guid userId))
                {
                    id = userId;
                    HttpContext.Session.SetString("Id", id.ToString());
                }

                if (id != Guid.Empty)
                {
                    HttpContext.Session.SetString("Username", AddUser.Username);
                    HttpContext.Session.SetString("Avatar", AddUser.Avatar);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Mật khẩu hoặc Tên người dùng không chính xác";
                return RedirectToAction("Login");
            }
        }

        public async Task<bool> AuthenticateUserAsync(string Username, string Password)
        {
            var user = await _mvcContextDb.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);
            return (user != null);
        }
    }
}