using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Models;
using blog.Helpers;
using PagedList.Core;
using Microsoft.AspNetCore.Authorization;

namespace blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class PostsController : Controller
    {
        private readonly BlogDbContext _context;

        public PostsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index(int? page)
        {
            if (!User.Identity.IsAuthenticated) Response.Redirect("/dang-nhap.html");
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null) return RedirectToAction("Login", "Accounts", new { Area = "Admin" });
            var account = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.AccountId == int.Parse(taikhoanID));
            if (account == null) return NotFound();

            List<Post> lsPost = new List<Post>();
            var pageNumber = page == null || page <= 0 ? 1 :page.Value;
            var pageSize = 5;
            if(account.RoleId == 2)
            {
                lsPost = _context.Posts.Include(x => x.Account).Include(x => x.Category).Where(x => x.AccountId == account.AccountId).OrderByDescending(x => x.CategoryId).ToList();
            }
            else
            {
                lsPost = _context.Posts.Include(x => x.Account).Include(x => x.Category).OrderByDescending(x => x.CategoryId).ToList();
            }

         /*   var blogDbContext = _context.Posts.Include(p => p.Account).Include(p => p.Category);*/
           /* return View(await blogDbContext.ToListAsync());*/
            PagedList<Post> models = new PagedList<Post>(lsPost.AsQueryable(),pageNumber,pageSize);
            return View(models);
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
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

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
     
            // kiểm tra quyền truy cập
            if (!User.Identity.IsAuthenticated) Response.Redirect("/dang-nhap.html");
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if(taikhoanID == null ) return RedirectToAction("Login", "Accounts", new {Area = "Admin"});
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,ShortContents,Contents,Thumb,Alias,Author,Tags,CategoryId,Published,IsHot,AccountId,IsNewFeed,CreatedDate")] Post post, IFormFile fThumb)
        {
                if (!User.Identity.IsAuthenticated) Response.Redirect("/dang-nhap.html");
                var taikhoanID = HttpContext.Session.GetString("AccountId");
                if (taikhoanID == null) return RedirectToAction("Login", "Accounts", new { Area = "Admin" });
                var account = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.AccountId == int.Parse(taikhoanID));
            if (account == null) return NotFound();
            if (ModelState.IsValid)
            {
                post.AccountId = account.AccountId;
                post.Author = account.FullName;
                if (post.CategoryId == null) post.CategoryId = 1;
                post.CreatedDate = DateTime.Now;
                post.Alias = Utilities.REG(post.Title);
                post.Views = 0;
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string Newname = Utilities.REG(post.Title) + extension;
                    post.Thumb = await Utilities.UpLoadFile(fThumb, @"new\", Newname.ToLower());
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }


            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,ShortContents,Contents,Thumb,Alias,Author,Tags,CategoryId,Published,IsHot,AccountId,IsNewFeed,Views")] Post post, IFormFile fThumb)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }
            if (!User.Identity.IsAuthenticated) Response.Redirect("/dang-nhap.html");
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null) return RedirectToAction("Login", "Accounts", new { Area = "Admin" });
            var account = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.AccountId == int.Parse(taikhoanID));
            if (account == null) return NotFound();

            if(account.RoleId != 7)
            {
                if (post.AccountId != account.AccountId) return RedirectToAction(nameof(Index));
            }

        


            if (ModelState.IsValid)

                if (ModelState.IsValid)
            {
                try
                {
                        post.AccountId = account.AccountId;
                        post.Author = account.FullName;
                        if (post.CategoryId == null) post.CategoryId = 1;
                        post.CreatedDate = DateTime.Now;
                        post.Alias = Utilities.REG(post.Title);
                        post.Views = 0;
                        if (fThumb != null)
                        {
                            string extension = Path.GetExtension(fThumb.FileName);
                            string Newname = Utilities.REG(post.Title) + extension;
                            post.Thumb = await Utilities.UpLoadFile(fThumb, @"new\", Newname.ToLower());
                        }
                        _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", post.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", post.CategoryId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
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

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'BlogDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
