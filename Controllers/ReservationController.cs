using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airline_Reservation_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Airline_Reservation_System.Controllers
{
    public class ReservationController : Controller
    {


        private readonly ReservationContext _context;

        public ReservationController(ReservationContext context)
        {
            _context = context;
        }

        // GET: Reservation
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Reservations.ToListAsync());
        //}
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string searchString1)
        {
            ViewData["FnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
            ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewData["LnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "Lname";
            ViewData["PassportSortParm"] = sortOrder == "Passport" ? "passport_desc" : "Passport";
            ViewData["CurrentFilter"] = searchString;
            var testsort = from s in _context.Reservations
                           select s;

            int n;
            bool isNumeric = int.TryParse(searchString, out n);

            if (!String.IsNullOrEmpty(searchString) && !isNumeric)
            {
                testsort = testsort.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));

            }

            if (!String.IsNullOrEmpty(searchString) && isNumeric)
            {
                testsort = testsort.Where(s => s.PassportNumber == n
                                       || s.CellphoneNumber == n);
            }

            switch (sortOrder)
            {
                case "Passport":
                    testsort = testsort.OrderBy(s => s.PassportNumber);
                    break;
                case "passport_desc":
                    testsort = testsort.OrderByDescending(s => s.PassportNumber);
                    break;
                case "Lname":
                    testsort = testsort.OrderBy(s => s.LastName);
                    break;
                case "lname_desc":
                    testsort = testsort.OrderByDescending(s => s.LastName);
                    break;
                case "fname_desc":
                    testsort = testsort.OrderByDescending(s => s.FirstName);
                    break;
                case "Phone":
                    testsort = testsort.OrderBy(s => s.CellphoneNumber);
                    break;
                case "phone_desc":
                    testsort = testsort.OrderByDescending(s => s.CellphoneNumber);
                    break;
                default:
                    testsort = testsort.OrderBy(s => s.FirstName);
                    break;
            }
            return View(await testsort.AsNoTracking().ToListAsync());
        }



        // //////////////////////////////GET: Reservation/Create
        [Authorize]
        public IActionResult New(int id = 0)

        {
            if (id == 0)
                return View(new Reservation());
            else
                return View(_context.Reservations.Find(id));
        }
        ///////////////////////////////////
        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([Bind("ReservationId,FirstName,LastName,PassportNumber,CellphoneNumber")] Reservation reservation)

        {
            if (ModelState.IsValid)
            {
                if (reservation.ReservationId == 0)
                    _context.Add(reservation);
                else
                    _context.Update(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }



        // GET: Reservation/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}

    }
}
