﻿using System;
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
    public class CapacitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CapacitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Capacities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Capacities.ToListAsync());
        }

        // GET: Capacities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacity = await _context.Capacities
                .FirstOrDefaultAsync(m => m.CapacityId == id);
            if (capacity == null)
            {
                return NotFound();
            }

            return View(capacity);
        }

        // GET: Capacities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Capacities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipeOfWasteId,MaxKg")] Capacity capacity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(capacity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(capacity);
        }

        // GET: Capacities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacity = await _context.Capacities.FindAsync(id);
            if (capacity == null)
            {
                return NotFound();
            }
            return View(capacity);
        }

        // POST: Capacities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CapacityId,TipeOfWasteId,MaxKg")] Capacity capacity)
        {
            if (id != capacity.CapacityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(capacity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CapacityExists(capacity.CapacityId))
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
            return View(capacity);
        }

        // GET: Capacities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var capacity = await _context.Capacities
                .FirstOrDefaultAsync(m => m.CapacityId == id);
            if (capacity == null)
            {
                return NotFound();
            }

            return View(capacity);
        }

        // POST: Capacities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var capacity = await _context.Capacities.FindAsync(id);
            if (capacity != null)
            {
                _context.Capacities.Remove(capacity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CapacityExists(int id)
        {
            return _context.Capacities.Any(e => e.CapacityId == id);
        }
    }
}