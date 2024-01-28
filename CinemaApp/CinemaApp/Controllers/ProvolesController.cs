using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;
using System.Diagnostics;

namespace CinemaApp.Controllers
{
    public class ProvolesController : Controller
    {
        private readonly DBContext _context;

        public ProvolesController(DBContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var listofData = _context.Provoles.ToList();
            return View(listofData);
        }
    }
}
