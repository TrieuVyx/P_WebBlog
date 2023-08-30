using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using blog.Helpers;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;

namespace blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostssController : Controller
    {
        private readonly BlogDbContext _context;

        public PostssController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Accounts

        //public async Task<IActionResult> Index(int? page)
        //{
        //    string accountId = HttpContext.Session.GetString("AccountId");
        //    if (!string.IsNullOrEmpty(accountId))
        //    {
        //        var account = _context.Accounts.FirstOrDefault(a => a.AccountId.ToString() == accountId);
        //        if (account != null)
        //        {
        //            return View(account);
        //        }
        //    }
        //    return View();
        //}
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 7;
            var lsRoles = _context.Posts.OrderByDescending(x => x.PostId);
            var posts = _context.Posts.Include(p => p.Category).ToList();
            var models = new PagedList<Post>(lsRoles, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
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
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
            {
                return RedirectToAction("Login", "User", new { Area = "" });
            }

            // Tạo một đối tượng Post mới
            var post = new Post();

            // Gán giá trị cho thuộc tính AccountId của đối tượng post
            post.AccountId = int.Parse(taikhoanID);

            // Truyền dữ liệu cho view
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");

            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post, IFormFile fThumb, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            var taikhoanid = HttpContext.Session.GetString("AccountId");
            var accountid = _context.Accounts.FirstOrDefault(x => x.AccountId == int.Parse(taikhoanid));
            post.AccountId = accountid?.AccountId;

            if (fThumb != null && fThumb.Length > 0)
            {
                string fileName = Path.GetFileName(fThumb.FileName);
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fThumb.CopyToAsync(stream);
                }

                post.Thumb = fileName;
            }

            try
            {
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi tạo bài viết: " + ex.Message);
                return View(post);
            }
        }
        private List<string> GetCategoriesFromDataSource()
        {
            throw new NotImplementedException();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post updatedPost, IFormFile fThumb, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            // Kiểm tra tính hợp lệ của ModelState
            if (ModelState.IsValid)
            {
                try
                {
                    var existingPost = await _context.Posts.FindAsync(id);

                    if (existingPost != null)
                    {
                        existingPost.Title = updatedPost.Title;
                        existingPost.ShortContents = updatedPost.ShortContents;
                        existingPost.Contents = updatedPost.Contents;
                        existingPost.Thumb = updatedPost.Thumb;
                        existingPost.Alias = updatedPost.Alias;
                        existingPost.Author = updatedPost.Author;
                        existingPost.Tags = updatedPost.Tags;
                        existingPost.CategoryId = updatedPost.CategoryId;
                        existingPost.Published = updatedPost.Published;
                        existingPost.IsHot = updatedPost.IsHot;
                        existingPost.AccountId = updatedPost.AccountId;
                        existingPost.IsNewFeed = updatedPost.IsNewFeed;
                        existingPost.CreatedDate = updatedPost.CreatedDate;
                        existingPost.Views = updatedPost.Views;
                        existingPost.Account = updatedPost.Account;
                        existingPost.Category = updatedPost.Category;

                        if (fThumb != null && fThumb.Length > 0)
                        {
                            string fileName = Path.GetFileName(fThumb.FileName);
                            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                            string filePath = Path.Combine(uploadsFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await fThumb.CopyToAsync(stream);
                            }

                            // Xóa tập tin hình ảnh cũ (nếu có)
                            if (!string.IsNullOrEmpty(existingPost.Thumb))
                            {
                                string oldFilePath = Path.Combine(uploadsFolder, existingPost.Thumb);
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                            }

                            existingPost.Thumb = fileName;
                        }

                        // Lưu các thay đổi vào cơ sở dữ liệu
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không tìm thấy bài viết cần chỉnh sửa.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật bài viết: " + ex.Message);
                }
            }

            return View(updatedPost);
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
