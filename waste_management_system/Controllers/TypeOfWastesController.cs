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
    public class TypeOfWastesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeOfWastesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeOfWastes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TypeOfWastes.Include(t => t.Vehicles);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TypeOfWastes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfWaste = await _context.TypeOfWastes
                .Include(t => t.Vehicles)
                .FirstOrDefaultAsync(m => m.TypeOfWasteId == id);
            if (typeOfWaste == null)
            {
                return NotFound();
            }

            return View(typeOfWaste);
        }

        // GET: TypeOfWastes/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleClass");
            return View();
        }

        // POST: TypeOfWastes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeOfWasteId,Name,Description,Value,VehicleId")] TypeOfWaste typeOfWaste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfWaste);
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleClass", typeOfWaste.VehicleId);
            return View(typeOfWaste);
        }

        // GET: TypeOfWastes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfWaste = await _context.TypeOfWastes.FindAsync(id);
            if (typeOfWaste == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleClass", typeOfWaste.VehicleId);
            return View(typeOfWaste);
        }

        // POST: TypeOfWastes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeOfWasteId,Name,Description,Value,VehicleId")] TypeOfWaste typeOfWaste)
        {
            if (id != typeOfWaste.TypeOfWasteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfWaste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfWasteExists(typeOfWaste.TypeOfWasteId))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleClass", typeOfWaste.VehicleId);
            return View(typeOfWaste);
        }

        // GET: TypeOfWastes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfWaste = await _context.TypeOfWastes
                .Include(t => t.Vehicles)
                .FirstOrDefaultAsync(m => m.TypeOfWasteId == id);
            if (typeOfWaste == null)
            {
                return NotFound();
            }

            return View(typeOfWaste);
        }

        // POST: TypeOfWastes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfWaste = await _context.TypeOfWastes.FindAsync(id);
            if (typeOfWaste != null)
            {
                _context.TypeOfWastes.Remove(typeOfWaste);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfWasteExists(int id)
        {
            return _context.TypeOfWastes.Any(e => e.TypeOfWasteId == id);
        }
    }
}
