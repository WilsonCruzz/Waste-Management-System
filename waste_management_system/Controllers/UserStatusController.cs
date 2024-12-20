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
    public class UserStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserStatuses.ToListAsync());
        }

        // GET: UserStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStatus = await _context.UserStatuses
                .FirstOrDefaultAsync(m => m.UserStatuId == id);
            if (userStatus == null)
            {
                return NotFound();
            }

            return View(userStatus);
        }

        // GET: UserStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeOfUser,Status")] UserStatus userStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userStatus);
        }

        // GET: UserStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStatus = await _context.UserStatuses.FindAsync(id);
            if (userStatus == null)
            {
                return NotFound();
            }
            return View(userStatus);
        }

        // POST: UserStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserStatuId,TypeOfUser,Status")] UserStatus userStatus)
        {
            if (id != userStatus.UserStatuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStatusExists(userStatus.UserStatuId))
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
            return View(userStatus);
        }

        // GET: UserStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userStatus = await _context.UserStatuses
                .FirstOrDefaultAsync(m => m.UserStatuId == id);
            if (userStatus == null)
            {
                return NotFound();
            }

            return View(userStatus);
        }

        // POST: UserStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userStatus = await _context.UserStatuses.FindAsync(id);
            if (userStatus != null)
            {
                _context.UserStatuses.Remove(userStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserStatusExists(int id)
        {
            return _context.UserStatuses.Any(e => e.UserStatuId == id);
        }
    }
}
