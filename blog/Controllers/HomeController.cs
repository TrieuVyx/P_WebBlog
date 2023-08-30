using Azure;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PagedList.Core;
using System.Diagnostics;
using System.Drawing.Printing;

namespace blog.Controllers
{
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
        private List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();

            Post post1 = new Post();

            posts.Add(post1);

            return posts;
        }

        public IActionResult Index()
        {
            string accountId = HttpContext.Session.GetString("AccountId");
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                if (account != null)
                {
                    var posts = _context.Posts.ToList();
                    var viewModel = new PostAccountView
                    {
                        Posts = posts,
                        Account = account
                    };

                    return View(viewModel);
                }
            }

            var postsWithoutAccount = _context.Posts.ToList();
            var defaultViewModel = new PostAccountView
            {
                Posts = postsWithoutAccount,
                Account = null
            };

            return View(defaultViewModel);
        }
        public IActionResult Tech()
        {
            string accountId = HttpContext.Session.GetString("AccountId");
            var sport = "Sport";
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                if (account != null)
                {
                    var posts = _context.Posts.Where(p => p.CategoryId == 1).ToList(); // Thay thế CategoryId bằng ID "Sport"
                    var viewModel = new PostAccountView
                    {
                        Posts = posts,
                        Account = account
                    };

                    return View(viewModel);
                }
            }

            var postsWithoutAccount = _context.Posts.Where(p => p.CategoryId == 1).ToList(); // Thay thế CategoryId bằng ID "Sport"
            var defaultViewModel = new PostAccountView
            {
                Posts = postsWithoutAccount,
                Account = null
            };

            return View(defaultViewModel);
        }
        public IActionResult Food()
        {
            string accountId = HttpContext.Session.GetString("AccountId");
            var sport = "Sport";
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                if (account != null)
                {
                    var posts = _context.Posts.Where(p => p.CategoryId == 2).ToList(); // Thay thế CategoryId bằng ID "Sport"
                    var viewModel = new PostAccountView
                    {
                        Posts = posts,
                        Account = account
                    };

                    return View(viewModel);
                }
            }

            var postsWithoutAccount = _context.Posts.Where(p => p.CategoryId == 2).ToList(); // Thay thế CategoryId bằng ID "Sport"
            var defaultViewModel = new PostAccountView
            {
                Posts = postsWithoutAccount,
                Account = null
            };

            return View(defaultViewModel);
        }
        public IActionResult Sport()
        {
            string accountId = HttpContext.Session.GetString("AccountId");
            var sport = "Sport";
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                if (account != null)
                {
                    var posts = _context.Posts.Where(p => p.CategoryId == 3).ToList(); // Thay thế CategoryId bằng ID "Sport"
                    var viewModel = new PostAccountView
                    {
                        Posts = posts,
                        Account = account
                    };

                    return View(viewModel);
                }
            }

            var postsWithoutAccount = _context.Posts.Where(p => p.CategoryId == 3).ToList(); // Thay thế CategoryId bằng ID "Sport"
            var defaultViewModel = new PostAccountView
            {
                Posts = postsWithoutAccount,
                Account = null
            };

            return View(defaultViewModel);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            string accountId = HttpContext.Session.GetString("AccountId");

            if (!string.IsNullOrEmpty(accountId))
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId.ToString() == accountId);

                if (account != null)
                {
                    var posts = await _context.Posts.ToListAsync();
                    var viewModel = new PostAccountView
                    {
                        Posts = posts,
                        Account = account
                    };

                    return View(viewModel);
                }
            }

            var post = await _context.Posts
                .Include(p => p.Account)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        public IActionResult Category()
        {
            string accountId = HttpContext.Session.GetString("AccountId");
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
                if (account != null)
                {
                    var posts = _context.Posts.ToList();
                    var viewModel = new PostAccountView
                    {
                        Posts = posts,
                        Account = account
                    };

                    return View(viewModel);
                }
            }

            var postsWithoutAccount = _context.Posts.ToList();
            var defaultViewModel = new PostAccountView
            {
                Posts = postsWithoutAccount,
                Account = null
            };

            return View(defaultViewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}