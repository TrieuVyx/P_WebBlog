using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using blog.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using blog.Helpers;
using Microsoft.AspNetCore.Authorization;
using blog.Extension;
using blog.Areas.Admin.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PagedList.Core;
using System.Drawing.Printing;
using System.Data;

namespace blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")] 
    
    public class AccountsController : Controller
    {
        private readonly BlogDbContext _context;

        public AccountsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            var accounts = await _context.Accounts.ToListAsync();
            return View(accounts);
        }

        // login
        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            var taiKhoanID = HttpContext.Session.GetString("AccountId");
            if (taiKhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        /*  [ValidateAntiForgeryToken]*/
        //[Route("dang-nhap.html", Name = "Login")]
        //public async Task<IActionResult> Login() { 

        //}
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Account kh = _context.Accounts.Include(p => p.Role).SingleOrDefault(p => p.Email.ToLower() == model.Email.ToLower().Trim());
        //            if (kh == null)
        //            {
        //                ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
        //                return View(model);
        //            }
        //            string pass = model.Password.Trim().ToMD5();

        //            if (kh.Password.Trim() != pass)
        //            {
        //                ViewBag.Error = "Thông tin đăng nhập chưa chính xác";
        //                return View(model);
        //            }
        //            kh.LastLogin = DateTime.Now;

        //            _context.Update(kh);
        //            await _context.SaveChangesAsync();

        //            var taiKhoanID = HttpContext.Session.GetString("AccountId");
        //            HttpContext.Session.SetString("AccountId", kh.AccountId.ToString());

        //            var userClaims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Name,kh.FullName),
        //                new Claim(ClaimTypes.Email,kh.Email),
        //                new Claim("AccountId",kh.AccountId.ToString()),
        //                new Claim("RoleId",kh.RoleId.ToString()),
        //                new Claim(ClaimTypes.Role,kh.Role.RoleName)
        //            };

        //            var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
        //            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
        //            await HttpContext.SignInAsync(userPrincipal);
        //            if (Url.IsLocalUrl(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //        }
        //        return RedirectToAction("Index", "Home", new { Area = "Admin" });
        //    }
        //    catch
        //    {

        //        return RedirectToAction("Login", "", new { Area = "Admin" });

        //    }

        //}

        [Route("dang-xuat.html", Name = "Logout")]
        public IActionResult logout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("AccountId");
                return RedirectToAction("Login", "User");   
            }
            catch
            {
                return RedirectToAction("Login", "User");
            }
        }



        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleName", "RoleName");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FullName,Email,Phone,Password,Salt,Active,CreatedDate,LastLogin,RoleId, ProfileImage")] Account account)
        {
            if (ModelState.IsValid)
            {
                List<Role> roles = _context.Roles.ToList();
                ViewBag.Role = new SelectList(roles, "RoleId", "RoleName");

                string ProfileImage = account.ProfileImage;
                //string salt = Utilities.GetRanDomKey(10);
                string hashedPassword = HashPasswordMD5.ToMD5(account.Password);
                account.Password = hashedPassword;
                //account.Salt = salt;
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);

            return View(account);
        }
    
        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.Role);
            ViewData["RoleDescription"] = new SelectList(_context.Roles, "RoleId", "RoleDescription", account.Role);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,FullName,Email,Phone,Password,Salt,Active,CreatedDate,LastLogin,RoleId")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'BlogDbContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}