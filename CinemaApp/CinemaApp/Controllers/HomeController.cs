using CinemaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CinemaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;

        public HomeController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verify(User usermodel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == usermodel.Username);

                if (user != null && VerifyPassword(usermodel.Password, user.Salt, user.Password) && user.Role == "admin")
                {
                    return RedirectToAction("Index", "ContentAdmins");
                }
                else if (user != null && VerifyPassword(usermodel.Password, user.Salt, user.Password) && user.Role == "contentadmin")
                {
                    // User exists and is either content admin or customer, redirect to SuccessView
                    return View("SuccessView");
                }
                else if (user != null && VerifyPassword(usermodel.Password, user.Salt, user.Password) && user.Role == "customer")
                {
                    // User exists and is either content admin or customer, redirect to SuccessView
                    return View("SuccessView");
                }

            }
            return View("ErrorView");
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


        //HashPassword
        private bool VerifyPassword(string enteredPassword, string salt, string hashedPasswordFromDb)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword, salt);
            return hashedEnteredPassword == hashedPasswordFromDb;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //customer registration
        public IActionResult Registration(string username, string password, string email)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string salt = GenerateSalt();

                    string hashedPassword = HashPassword(password, salt);

                    var user = new User
                    {
                        Username = username,
                        Password = hashedPassword,
                        Email = email,
                        Salt = salt,
                        Role = "customer"
                    };

                    var customer = new Customer
                    {
                        Name = username,
                        UserUsernameNavigation = user
                    };

                    _context.Users.Add(user);
                    _context.Customers.Add(customer);

                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    return View("Registration");
                }
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

        
    }
}
