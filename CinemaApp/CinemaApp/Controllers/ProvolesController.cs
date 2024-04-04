using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;
using System.Diagnostics;
using System;
using System.Linq;

namespace CinemaApp.Controllers
{
    public class ProvolesController : Controller
    {
        private readonly DBContext _context;

        public ProvolesController(DBContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Provoles.Include(m => m.ContentAdmin).Include(m => m.Cinemas)
                           .Include(m => m.Movies);
            var provolesList = await dBContext.ToListAsync();
      
            return View(provolesList);
        }
       

        [HttpGet]
        public IActionResult AddScreening()
        {
            ViewBag.CinemasList = new SelectList(_context.Cinemas, "Id", "Name");
            ViewBag.MoviesList = new SelectList(_context.Movies, "Id", "Name");
            ViewBag.ContentAdminsList = new SelectList(_context.ContentAdmins, "Id", "Name");
            return View();
        }
        //POST:Movies/AddScreening
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddScreening([Bind("MoviesTime,CinemasId,MoviesId,ContentAdminId")] Provole screening)
        {
            ModelState.Remove("Cinemas");
            ModelState.Remove("Movies");
            ModelState.Remove("ContentAdmin");
            try
            {


                if (ModelState.IsValid)
                {
                    var cinema = _context.Cinemas.Find(screening.CinemasId);
                    if (cinema != null)
                    {
                        var provole = new Provole
                        {
                            MoviesTime = screening.MoviesTime,
                            CinemasId = screening.CinemasId,
                            MoviesId = screening.MoviesId,
                            ContentAdminId = screening.ContentAdminId,
                            Seats = cinema.Seats
                        };

                        _context.Provoles.Add(provole);
                        _context.SaveChanges();

                        return View("Index", _context.Provoles.ToList());
                    }
                    else
                    {
                        return NotFound();
                    }
                  }
                  else
                  {
                      ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Name", screening.CinemasId);
                      ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name", screening.MoviesId);
                      ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Name", screening.ContentAdminId);

                      return View(screening);

                  }

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
          // GET: Provoles/Edit/5
          public async Task<IActionResult> Edit(int? id)
          {
              if (id == null)
              {
                  return NotFound();
              }

              var screening = await _context.Provoles.FindAsync(id);
              if (screening == null)
              {
                  return NotFound();
              }

              return View(screening);
          }


          // POST: Provoles/Edit/5
          // To protect from overposting attacks, enable the specific properties you want to bind to.
          // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          // POST: Provoles/Edit/5
          // To protect from overposting attacks, enable the specific properties you want to bind to.
          // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          // POST: Provoles/Edit/5
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Edit(int id, Provole screening)
          {
              Provole currentScreening = _context.Provoles.Find(id);

              if (currentScreening == null)
              {
                  return NotFound();
              }

              try
              {
                  // Update only the MoviesTime property
                  currentScreening.MoviesTime = screening.MoviesTime;

                  // Save the changes to the database
                  _context.SaveChanges();

                  return RedirectToAction("Index", "Provoles");
              }
              catch (Exception ex)
              {
                  // Log the exception or handle it appropriately
                  var errorViewModel = new ErrorViewModel
                  {
                      RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                      // Other properties as needed
                  };

                  ViewBag.ErrorMessage = "An error occurred while updating the screening.";
                  return View("Error", errorViewModel);
              }
          }




          /*
          if (ModelState.IsValid)
          {
              try
              {
                  _context.Provoles.Attach(screening);
                  _context.Entry(screening).Property(m => m.MoviesTime).IsModified = true;
                  // Date field is set to Modified (entity is now Modified as well)
                  _context.SaveChanges();
                  return RedirectToAction("Index"); 

              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!ScreeningExists(screening.Id))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }
          }
          return View(screening);
      }


          */


                    private bool ScreeningExists(int id)
        {
            return _context.Provoles.Any(e => e.Id == id);
        }
    }
}
