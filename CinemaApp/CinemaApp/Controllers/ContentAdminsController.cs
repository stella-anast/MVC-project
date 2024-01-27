using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;


namespace CinemaApp.Controllers
{

    public class ContentAdminsController : Controller
    {
        private readonly DBContext _context;

        public ContentAdminsController(DBContext context)
        {
            _context = context;
        }

        // GET: ContentAdmins
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.ContentAdmins.Include(c => c.UserUsernameNavigation);
            return View(await dBContext.ToListAsync());
        }

        // GET: ContentAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .Include(c => c.UserUsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }
        [HttpGet]
        public IActionResult CreateContentAdmin()
        {
            // Handle GET request to display the CreateContentAdmin form
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateContentAdmin(User userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userModel.Password == null)
                    {
                        // Handle the case where the password is null
                        ModelState.AddModelError("Password", "Password is required.");
                        return View("CreateContentAdmin", userModel);
                    }

                    string salt = GenerateSalt();
                    string hashedPassword = HashPassword(userModel.Password, salt);

                    var user = new User
                    {
                        Username = userModel.Username,
                        Password = hashedPassword,
                        Email = userModel.Email,
                        Salt = salt,
                        Role = "contentAdmin"
                    };

                    var contentAdmin = new ContentAdmin
                    {
                        Name = userModel.Username,
                        UserUsernameNavigation = user
                    };

                    _context.Users.Add(user);
                    _context.ContentAdmins.Add(contentAdmin);

                    _context.SaveChanges();

                    return RedirectToAction("Index", "ContentAdmins");
                }

                // If ModelState is not valid, it will fall through to the catch block
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    // Other properties as needed
                };

                ViewBag.ErrorMessage = "An error occurred during registration.";
                return View("Error", errorViewModel);
            }

            // ModelState is not valid, return the view with the userModel to display validation errors
            return View("CreateContentAdmin", userModel);
        }





        // method to hash password with salt using SHA256
        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var hashedBytes = sha256.ComputeHash(saltedPassword);
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // method to generate a random salt
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // GET: ContentAdmins/Create
        public IActionResult Create()
        {
            ViewData["UserUsername"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: ContentAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserUsername")] ContentAdmin contentAdmin)

        {
            if (ModelState.IsValid)
            {
                _context.Add(contentAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserUsername"] = new SelectList(_context.Users, "Username", "Username", contentAdmin.UserUsername);
            return View(contentAdmin);
        }

        // GET: ContentAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin == null)
            {
                return NotFound();
            }
            ViewData["UserUsername"] = new SelectList(_context.Users, "Username", "Username", contentAdmin.UserUsername);
            return View(contentAdmin);
        }

        // POST: ContentAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserUsername")] ContentAdmin contentAdmin)
        {
            if (id != contentAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentAdminExists(contentAdmin.Id))
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
            ViewData["UserUsername"] = new SelectList(_context.Users, "Username", "Username", contentAdmin.UserUsername);
            return View(contentAdmin);
        }
        private bool ContentAdminExists(int id)
        {
            return _context.ContentAdmins.Any(e => e.Id == id);
        }


        // GET: ContentAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .Include(c => c.UserUsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // POST: ContentAdmins/Delete/5
        // POST: ContentAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contentAdmin = await _context.ContentAdmins.FindAsync(id);

            if (contentAdmin == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == contentAdmin.UserUsername);

            if (user != null)
            {
                _context.Users.Remove(user);
            }

            _context.ContentAdmins.Remove(contentAdmin);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
