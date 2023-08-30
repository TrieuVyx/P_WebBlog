using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Models;
using Azure;
using PagedList.Core;
using blog.Helpers;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly BlogDbContext _context;


        public CategoriesController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 7;
            var lsCategories = _context.Categories.OrderByDescending(x => x.CategoryId);
            PagedList<Category> models = new PagedList<Category>(lsCategories, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        /*    return _context.Categories != null ?
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'BlogDbContext.Categories'  is null.");*/
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        //public IActionResult Create()
        //{
        //    ViewData["CategoryName"] = new SelectList(_context.Categories.Where(x => x.Levels == 1), "CategoryId", "CategoryName");
        //    return View();
        //}
        public IActionResult Create()
        {
            var categories = _context.Categories.Where(x => x.Levels == 1)
                                                .Select(x => new SelectListItem
                                                {
                                                    Value = x.CategoryId.ToString(),
                                                    Text = x.CategoryName
                                                })
                                                .ToList();

            ViewData["Categories"] = categories;

            return View();
        }
        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Title,Alias,MetaDesc,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, IFormFile fThumb, IFormFile fIcon, IFormFile fCover)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        category.Alias = Utilities.REG(category.CategoryName);
        //        if (category.Parents == null)
        //        {
        //            category.Levels = 1;

        //        }
        //        else
        //        {
        //            category.Levels = category.Parents == 0 ? 1 : 2;

        //        }
        //        if (fThumb != null)
        //        {
        //            string extension = Path.GetExtension(fThumb.FileName);
        //            string Newname = Utilities.REG(category.CategoryName) + "preview_" + extension;
        //            category.Thumb = await Utilities.UpLoadFile(fThumb, @"categories\", Newname.ToLower());
        //        }
        //        if (fCover != null)
        //        {
        //            string extension = Path.GetExtension(fCover.FileName);
        //            string Newname = "cover_" + Utilities.REG(category.CategoryName) + extension;
        //            category.Cover = await Utilities.UpLoadFile(fCover, @"covers\", Newname.ToLower());
        //        }
        //        if (fIcon != null)
        //        {
        //            string extension = Path.GetExtension(fIcon.FileName);
        //            string Newname = "icon_" + Utilities.REG(category.CategoryName) + extension;
        //            category.Icon = await Utilities.UpLoadFile(fIcon, @"icons\", Newname.ToLower());
        //        }

        //        _context.Add(category);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}

        [HttpPost]
        
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Alias = Utilities.REG(category.CategoryName);

                // Thực hiện lưu danh mục vào cơ sở dữ liệu
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Accounts"] = new SelectList(_context.Accounts, "AccountId", "FullName");
            return View(category);

        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Title,Alias,MetaDesc,MetaKey,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category, IFormFile fThumb, IFormFile fIcon, IFormFile fCover)
        //{
        //    if (id != category.CategoryId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try

        //        {
        //            category.Alias = Utilities.REG(category.CategoryName);
        //            if (category.Parents == null)
        //            {
        //                category.Levels = 1;

        //            }
        //            else
        //            {
        //                category.Levels = category.Parents == 0 ? 1 : 2;

        //            }
        //            if (fThumb != null)
        //            {
        //                string extension = Path.GetExtension(fThumb.FileName);
        //                string Newname = Utilities.REG(category.CategoryName) + "preview_" + extension;
        //                category.Thumb = await Utilities.UpLoadFile(fThumb, @"categories\", Newname.ToLower());
        //            }
        //            if (fCover != null)
        //            {
        //                string extension = Path.GetExtension(fCover.FileName);
        //                string Newname = "cover_" + Utilities.REG(category.CategoryName) + extension;
        //                category.Cover = await Utilities.UpLoadFile(fCover, @"covers\", Newname.ToLower());
        //            }
        //            if (fIcon != null)
        //            {
        //                string extension = Path.GetExtension(fIcon.FileName);
        //                string Newname = "icon_" + Utilities.REG(category.CategoryName) + extension;
        //                category.Icon = await Utilities.UpLoadFile(fIcon, @"icons\", Newname.ToLower());
        //            }

        //            _context.Update(category);
        //            await _context.SaveChangesAsync();

        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CategoryExists(category.CategoryId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}


        [HttpPost]
        public IActionResult Edit(int id, Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy danh mục từ cơ sở dữ liệu dựa trên id
                    var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

                    if (category == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật các thuộc tính của danh mục với dữ liệu từ model
                    category.CategoryName = model.CategoryName;
                    category.Title = model.Title;
                    category.Alias = model.Alias;
                    category.MetaDesc = model.MetaDesc;
                    category.MetaKey = model.MetaKey;
                    category.Thumb = model.Thumb;
                    category.Icon = model.Icon;
                    category.Cover = model.Cover;
                    category.Description = model.Description;
                    category.Published = model.Published;
                    category.Ordering = model.Ordering;
                    category.Parents = model.Parents;
                    category.Levels = model.Levels;

                    // Thực hiện lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();

                    // Chuyển hướng đến trang xem chi tiết danh mục
                    return RedirectToAction("Index", new { id = category.CategoryId });
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi, ví dụ: ghi log, hiển thị thông báo lỗi, vv.
                    // Trong ví dụ này, chúng ta chỉ đơn giản in ra thông báo lỗi
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Error");
                }
            }

            // Nếu ModelState không hợp lệ, trả về lại view "Edit" với model để hiển thị lỗi
            return View("Edit", model);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'BlogDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
