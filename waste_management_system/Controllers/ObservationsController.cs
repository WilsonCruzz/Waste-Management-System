using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using waste_management_system.Data;
using waste_management_system.Models;

namespace waste_management_system.Controllers
{
    [Authorize]
    public class ObservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Observations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Observations.Include(o => o.PickUpRequests);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Observations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _context.Observations
                .Include(o => o.PickUpRequests)
                .FirstOrDefaultAsync(m => m.ObservationId == id);
            if (observation == null)
            {
                return NotFound();
            }

            return View(observation);
        }

        // GET: Observations/Create
        public IActionResult Create()
        {
            ViewData["PickUpRequestId"] = new SelectList(_context.PickUpRequests, "PickUpRequestId", "PickUpRequestId");
            return View();
        }

        // POST: Observations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorName,Description,EntryDate,PickUpRequestId")] Observation observation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(observation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // error msg
            else
            {
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
            }
            ViewData["PickUpRequestId"] = new SelectList(_context.PickUpRequests, "PickUpRequestId", "PickUpRequestId", observation.PickUpRequestId);
            return View(observation);
        }

        // GET: Observations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _context.Observations.FindAsync(id);
            if (observation == null)
            {
                return NotFound();
            }
            ViewData["PickUpRequestId"] = new SelectList(_context.PickUpRequests, "PickUpRequestId", "PickUpRequestId", observation.PickUpRequestId);
            return View(observation);
        }

        // POST: Observations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObservationId,AuthorName,Description,EntryDate,PickUpRequestId")] Observation observation)
        {
            if (id != observation.ObservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(observation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObservationExists(observation.ObservationId))
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
            ViewData["PickUpRequestId"] = new SelectList(_context.PickUpRequests, "PickUpRequestId", "PickUpRequestId", observation.PickUpRequestId);
            return View(observation);
        }

        // GET: Observations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _context.Observations
                .Include(o => o.PickUpRequests)
                .FirstOrDefaultAsync(m => m.ObservationId == id);
            if (observation == null)
            {
                return NotFound();
            }

            return View(observation);
        }

        // POST: Observations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var observation = await _context.Observations.FindAsync(id);
            if (observation != null)
            {
                _context.Observations.Remove(observation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObservationExists(int id)
        {
            return _context.Observations.Any(e => e.ObservationId == id);
        }
    }
}
