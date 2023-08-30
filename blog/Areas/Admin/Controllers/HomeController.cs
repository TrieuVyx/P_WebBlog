using blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blog.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, BlogDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            string accountId = HttpContext.Session.GetString("AccountId");
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                if (account != null)
                {
                    return View(account);
                }
            }
            return View();
        }
    }
}
