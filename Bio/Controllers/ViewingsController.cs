using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bio.Data;
using Bio.Models;

namespace Bio.Controllers
{
    public class ViewingsController : Controller
    {
        private readonly CinemaContext _context;

        public ViewingsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Viewings
        public async Task<IActionResult> Index(string sortOrder)
        {
            var cinemaContext = _context.Viewings.Include(v => v.Auditorium).Include(v => v.Movie);
            ViewData["TimeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "time_desc" : "";
            ViewData["SeatsLeftSortParm"] = sortOrder == "SeatsLeft" ? "seatsleft_desc" : "SeatsLeft";
            var viewings = from v in _context.Viewings
                           select v;
            switch (sortOrder)
            {
                case "time_desc":
                    viewings = viewings.OrderByDescending(m => m.Time);
                    break;
                case "SeatsLeft":
                    viewings = viewings.OrderBy(m => m.SeatsLeft);
                    break;
                case "seatsleft_desc":
                    viewings = viewings.OrderByDescending(m => m.SeatsLeft);
                    break;
                default:
                    viewings = viewings.OrderBy(m => m.Time);
                    break;
            }
            var viewings2 = viewings.Include(v => v.Auditorium).Include(v => v.Movie);
            return View(await viewings2.AsNoTracking().ToListAsync());
        }

        // GET: Viewings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .Include(v => v.Auditorium)
                .Include(v => v.Movie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }

            return View(viewing);
        }

        // GET: Viewings/Create
        public IActionResult Create()
        {
            ViewData["AuditoriumID"] = new SelectList(_context.Auditoriums, "ID", "ID");
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID");
            return View();
        }

        // POST: Viewings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SeatsLeft,Time,MovieID,AuditoriumID")] Viewing viewing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuditoriumID"] = new SelectList(_context.Auditoriums, "ID", "ID", viewing.AuditoriumID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", viewing.MovieID);
            return View(viewing);
        }

        // GET: Viewings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings.SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }
            ViewData["AuditoriumID"] = new SelectList(_context.Auditoriums, "ID", "ID", viewing.AuditoriumID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", viewing.MovieID);
            return View(viewing);
        }

        // POST: Viewings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SeatsLeft,Time,MovieID,AuditoriumID")] Viewing viewing)
        {
            if (id != viewing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewingExists(viewing.ID))
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
            ViewData["AuditoriumID"] = new SelectList(_context.Auditoriums, "ID", "ID", viewing.AuditoriumID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", viewing.MovieID);
            return View(viewing);
        }

        // GET: Viewings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .Include(v => v.Auditorium)
                .Include(v => v.Movie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }

            return View(viewing);
        }

        // POST: Viewings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewing = await _context.Viewings.SingleOrDefaultAsync(m => m.ID == id);
            _context.Viewings.Remove(viewing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewingExists(int id)
        {
            return _context.Viewings.Any(e => e.ID == id);
        }

        // GET: Viewings/Book/5
        public async Task<IActionResult> Book(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings.SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }
            ViewData["AuditoriumID"] = new SelectList(_context.Auditoriums, "ID", "ID", viewing.AuditoriumID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", viewing.MovieID);
            return View(viewing);
        }

        // POST: Viewings/Book/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int id, [Bind("ID,SeatsLeft,Time,MovieID,AuditoriumID")] Viewing viewing)
        {
            if (id != viewing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (viewing.SeatsLeft > 0 && viewing.SeatsLeft <= 12)
                {
                    try
                    {
                        int NewSeats = (from s in _context.Viewings where s.ID == id select s.SeatsLeft).First() - viewing.SeatsLeft;
                        viewing.SeatsLeft = NewSeats;
                        _context.Update(viewing);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ViewingExists(viewing.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Confirmation), new { id, viewing.SeatsLeft, saveChangesError = true });
                }
                else
                {
                    ModelState.AddModelError("", "Can only book 1 to 12 tickets at a time.");
                }
            }
            ViewData["AuditoriumID"] = new SelectList(_context.Auditoriums, "ID", "ID", viewing.AuditoriumID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", viewing.MovieID);
            return View(viewing);
        }

        public async Task<IActionResult> Confirmation(int? id, int seatsLeft)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewing = await _context.Viewings
                .Include(v => v.Auditorium)
                .Include(v => v.Movie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (viewing == null)
            {
                return NotFound();
            }
            viewing.SeatsLeft = seatsLeft;
            return View(viewing);
        }
    }
}
