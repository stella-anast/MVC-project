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
          
            Debug.WriteLine($"screeningId: {screeningId}, username: {username}, NumberOfSeats: {reservation.NumberOfSeats}");
            try
            {
                ModelState.Remove("Customers");
                ModelState.Remove("Cinemas");
                ModelState.Remove("Provoles");             
                ModelState.Remove("ProvolesMovies");
                if (ModelState.IsValid)
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
                        _context.Attach(customer);

                        int availableSeats = screening.Seats;
                        int reservationSeats = reservation.NumberOfSeats;
                        if (reservationSeats > 0 && reservationSeats <= availableSeats)
                        {
                            int remainingSeats = availableSeats - reservationSeats;
                            screening.Seats = remainingSeats;
                            _context.Entry(reservation).Reference(r => r.Customers).Load();
                            customer = _context.Customers.Find(customer.Id);
                            Debug.WriteLine($"Customer ID: {customer.Id}");
                            _context.Entry(screening).Property(x => x.Seats).IsModified = true;
                            if (customer != null)
                            {
                                Debug.WriteLine($"Customer ID: {customer.Id}");
                                reservation.NumberOfSeats = reservationSeats;
                                reservation.CustomersId = customer.Id;
                                reservation.ProvolesMoviesId = screening.Id;
                                _context.Reservations.Add(reservation);
                                _context.Provoles.Update(screening);
                                _context.SaveChanges();
                                return RedirectToAction("SuccessView");

                            }
                            else
                            {
                                ViewBag.ErrorMessage = "User not found.";
                                return View("ErrorView");
                            }
                        }
                        else
                        {

                            ViewBag.ErrorMessage = "Not available seats.";
                            return View("ErrorView");
                        }

                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User not found.";
                        return View("ErrorView");

                    }
                }
                else
                {
                    return View("GetTickets", reservation);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                ViewBag.ErrorMessage = "An unexpected error occurred. Please try again later.";
                return View("ErrorView");
            }
            
        }
        [HttpGet]
        public IActionResult ShowHistory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ShowHistory(string username)
        {
            try {
                var reservations = _context.Reservations
               .Include(r => r.ProvolesMovies)
               .ThenInclude(p => p.Movies)
               .Where(r => r.Customers.UserUsername == username)
                .ToList();

                return View("showReservations", reservations);
            }
            catch
            {
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View("ErrorView");
            }
        }
        public IActionResult SuccessView()
        {
            return View();
        }
        public IActionResult ShowReservations(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    var reservation=_context.Reservations
                                    .Include(r=>r.ProvolesMovies)
                                    .ThenInclude(p=>p.Movies)
                                    .Include(r => r.CustomersId)
                                    .Where(r=>r.CustomersId == customer.Id)
                                    .ToList();
                    return View(reservation);

                }
                else
                {
                    ViewBag.ErrorMessage = "User not found";
                    return View("ErrorView");

                }
            }
            catch(Exception ex) 
            {
                ViewBag.ErrorMessage = "Error.Please try again.";
                return View("ErrorView");
            }
        }

    }
}
  

