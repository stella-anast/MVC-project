using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace CinemaApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly DBContext _context;


        public ReservationsController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowAvailability(int? screeningId)
        {
            if (screeningId == null)
            {
                return NotFound();
            }
            var screening = _context.Provoles
           .Include(p => p.Cinemas)
           .Include(p => p.Movies)
           .FirstOrDefault(p => p.Id == screeningId);

            if (screening == null)
            {
                return NotFound();
            }

            return View(screening);

        }
        [HttpGet]

        public IActionResult GetTickets(int? screeningId)
        {
            if (screeningId == null)
            {
                return NotFound();
            }



            ViewBag.ScreeningId = screeningId;

            return View();
        }
        [HttpPost]
        public IActionResult GetTickets(int? screeningId, [Bind("NumberOfSeats")] Reservation reservation, string username)
        {
            try
            {

                if (screeningId == null)
                {
                    return NotFound();
                }
                var screening = _context.Provoles
                              .Include(p => p.Cinemas)
                              .Include(p => p.Movies)
                              .FirstOrDefault(p => p.Id == screeningId);
                if (screening == null)
                {
                    return NotFound();
                }
                       
                   var customer = _context.Customers.FirstOrDefault(c => c.UserUsername == username);
                    if (customer != null)
                    {
                        var cinemaId = screening.CinemasId;
                        var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == cinemaId);
                        if (cinema != null)
                        {
                            int availableSeats = Int32.Parse(cinema.Seats);
                            int reservationSeats = reservation.NumberOfSeats;
                            if (reservationSeats > 0 && reservationSeats <= availableSeats)
                            {
                                int remainingSeats = availableSeats - reservationSeats;
                                cinema.Seats = remainingSeats.ToString();
                                _context.SaveChanges();
                                reservation.NumberOfSeats = reservationSeats;
                                reservation.CustomersId = customer.Id;
                                reservation.ProvolesMoviesId = screening.Id;
                                _context.Reservations.Add(reservation);
                                _context.Cinemas.Update(cinema);
                                _context.SaveChanges();
                                return RedirectToAction("SuccessView");
                            }
                            else
                            {

                                ViewBag.ErrorMessage = "Not available seats.";
                                return View("ErrorView");

                        }
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Cinema information not found.";
                            return View("ErrorView");

                    }
                    }
                    else
                    {

                        ViewBag.ErrorMessage = "User not found.";
                       return View("ErrorView");

                }

                
               
            }
            catch (Exception ex) { }
            ViewBag.ErrorMessage = "An unexpected error occurred. Please try again later.";
            return View("ErrorView");
        }

    }
}
  

