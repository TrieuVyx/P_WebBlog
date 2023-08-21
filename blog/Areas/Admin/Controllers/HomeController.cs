using Microsoft.AspNetCore.Mvc;

namespace blog.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]   
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
