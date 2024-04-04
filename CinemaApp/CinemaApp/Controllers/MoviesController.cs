using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;
using System.Diagnostics;


namespace CinemaApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DBContext _context;

        public MoviesController(DBContext context)
        {
            _context = context;
        }
        public IActionResult Add()
        {
            return RedirectToAction("AddScreening","Provoles");
        }
        public IActionResult ViewScreenings()
        {
            return RedirectToAction("Index", "Provoles");
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Movies.Include(m => m.ContentAdmin);
            return View(await dBContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.ContentAdmin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        [HttpGet]

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.ContentAdminId = new SelectList(_context.ContentAdmins, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Length,Type,Summary,Director,ReleaseYear,ContentAdminId")] Movie movie)
        {
            ModelState.Remove("ContentAdmin");
            try
            {
                

                if (ModelState.IsValid)
                {
                 
                    _context.Movies.Add(movie);
                    _context.SaveChanges();

                    return View("Index");
                }
                else
                {
                    ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Name", movie.ContentAdminId);

                    return View(movie);

                }
                /*
            if (ModelState.IsValid)
            {
            }
            else
            {
                // ModelState is not valid, handle the case
                //ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Name", movieModel.ContentAdminId);
                return View(movieModel);
            }
                */
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    // Other properties as needed
                };

                ViewBag.ErrorMessage = "An error occurred while saving the movie.";
                return View("Error", errorViewModel);
            }
        }



        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", movie.ContentAdminId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Type,Summary,Director,ReleaseYear,ContentAdminId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", movie.ContentAdminId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.ContentAdmin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
