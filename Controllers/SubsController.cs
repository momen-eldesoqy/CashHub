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
    public class SubsController : Controller
    {
        private readonly fawryContext _context;

        public SubsController(fawryContext context)
        {
            _context = context;
        }

        // GET: Subs
        public async Task<IActionResult> Index()
        {
            var fawryContext = _context.Subs.Include(s => s.Branch);
            return View(await fawryContext.ToListAsync());
        }

        // GET: Subs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subs == null)
            {
                return NotFound();
            }

            var sub = await _context.Subs
                .Include(s => s.Branch)
                .FirstOrDefaultAsync(m => m.SubId == id);
            if (sub == null)
            {
                return NotFound();
            }

            return View(sub);
        }

        // GET: Subs/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId");
            return View();
        }

        // POST: Subs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubId,SubName,SubAddress,SubRefNum,SubPhone,BranchId,CreatedOn,ModifiedOn")] Sub sub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sub);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId", sub.BranchId);
            return View(sub);
        }

        // GET: Subs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subs == null)
            {
                return NotFound();
            }

            var sub = await _context.Subs.FindAsync(id);
            if (sub == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId", sub.BranchId);
            return View(sub);
        }

        // POST: Subs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubId,SubName,SubAddress,SubRefNum,SubPhone,BranchId,CreatedOn,ModifiedOn")] Sub sub)
        {
            if (id != sub.SubId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubExists(sub.SubId))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId", sub.BranchId);
            return View(sub);
        }

        // GET: Subs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subs == null)
            {
                return NotFound();
            }

            var sub = await _context.Subs
                .Include(s => s.Branch)
                .FirstOrDefaultAsync(m => m.SubId == id);
            if (sub == null)
            {
                return NotFound();
            }

            return View(sub);
        }

        // POST: Subs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subs == null)
            {
                return Problem("Entity set 'fawryContext.Subs'  is null.");
            }
            var sub = await _context.Subs.FindAsync(id);
            if (sub != null)
            {
                _context.Subs.Remove(sub);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubExists(int id)
        {
          return (_context.Subs?.Any(e => e.SubId == id)).GetValueOrDefault();
        }
    }
}
