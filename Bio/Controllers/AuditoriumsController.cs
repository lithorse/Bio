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
    public class AuditoriumsController : Controller
    {
        private readonly CinemaContext _context;

        public AuditoriumsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Auditoriums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auditoriums.ToListAsync());
        }

        // GET: Auditoriums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditorium = await _context.Auditoriums
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auditorium == null)
            {
                return NotFound();
            }

            return View(auditorium);
        }

        // GET: Auditoriums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auditoriums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Seats")] Auditorium auditorium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auditorium);
        }

        // GET: Auditoriums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditorium = await _context.Auditoriums.SingleOrDefaultAsync(m => m.ID == id);
            if (auditorium == null)
            {
                return NotFound();
            }
            return View(auditorium);
        }

        // POST: Auditoriums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Seats")] Auditorium auditorium)
        {
            if (id != auditorium.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditoriumExists(auditorium.ID))
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
            return View(auditorium);
        }

        // GET: Auditoriums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditorium = await _context.Auditoriums
                .SingleOrDefaultAsync(m => m.ID == id);
            if (auditorium == null)
            {
                return NotFound();
            }

            return View(auditorium);
        }

        // POST: Auditoriums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditorium = await _context.Auditoriums.SingleOrDefaultAsync(m => m.ID == id);
            _context.Auditoriums.Remove(auditorium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditoriumExists(int id)
        {
            return _context.Auditoriums.Any(e => e.ID == id);
        }
    }
}
