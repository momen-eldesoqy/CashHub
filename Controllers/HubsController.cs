using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashHub.Models;

namespace CashHub.Controllers
{
    public class HubsController : Controller
    {
        private readonly fawryContext _context;

        public HubsController(fawryContext context)
        {
            _context = context;
        }

        // GET: Hubs
        public async Task<IActionResult> Index()
        {
              return _context.Hubs != null ? 
                          View(await _context.Hubs.ToListAsync()) :
                          Problem("Entity set 'fawryContext.Hubs'  is null.");
        }

        // GET: Hubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hubs == null)
            {
                return NotFound();
            }

            var hub = await _context.Hubs
                .FirstOrDefaultAsync(m => m.HubId == id);
            if (hub == null)
            {
                return NotFound();
            }

            return View(hub);
        }

        // GET: Hubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HubId,HubName,HubAddress,HunRefNum,Phone,CreatedOn,ModifiedOn")] Hub hub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hub);
        }

        // GET: Hubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hubs == null)
            {
                return NotFound();
            }

            var hub = await _context.Hubs.FindAsync(id);
            if (hub == null)
            {
                return NotFound();
            }
            return View(hub);
        }

        // POST: Hubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HubId,HubName,HubAddress,HunRefNum,Phone,CreatedOn,ModifiedOn")] Hub hub)
        {
            if (id != hub.HubId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HubExists(hub.HubId))
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
            return View(hub);
        }

        // GET: Hubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hubs == null)
            {
                return NotFound();
            }

            var hub = await _context.Hubs
                .FirstOrDefaultAsync(m => m.HubId == id);
            if (hub == null)
            {
                return NotFound();
            }

            return View(hub);
        }

        // POST: Hubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hubs == null)
            {
                return Problem("Entity set 'fawryContext.Hubs'  is null.");
            }
            var hub = await _context.Hubs.FindAsync(id);
            if (hub != null)
            {
                _context.Hubs.Remove(hub);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HubExists(int id)
        {
          return (_context.Hubs?.Any(e => e.HubId == id)).GetValueOrDefault();
        }
    }
}
