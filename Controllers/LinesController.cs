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
    public class LinesController : Controller
    {
        private readonly fawryContext _context;

        public LinesController(fawryContext context)
        {
            _context = context;
        }

        // GET: Lines
        public async Task<IActionResult> Index()
        {
            var fawryContext = _context.Lines.Include(l => l.LineId1Navigation);
            return View(await fawryContext.ToListAsync());
        }

        // GET: Lines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lines == null)
            {
                return NotFound();
            }

            var line = await _context.Lines
                .Include(l => l.LineId1Navigation)
                .FirstOrDefaultAsync(m => m.LineId == id);
            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        // GET: Lines/Create
        public IActionResult Create()
        {
            ViewData["LineId1"] = new SelectList(_context.Subs, "SubId", "SubId");
            return View();
        }

        // POST: Lines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineId,LineNumber,LineBalance,LineMaxAmount,CreatedOn,ModifiedOn,LineId1")] Line line)
        {
            if (ModelState.IsValid)
            {
                _context.Add(line);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId1"] = new SelectList(_context.Subs, "SubId", "SubId", line.LineId1);
            return View(line);
        }

        // GET: Lines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lines == null)
            {
                return NotFound();
            }

            var line = await _context.Lines.FindAsync(id);
            if (line == null)
            {
                return NotFound();
            }
            ViewData["LineId1"] = new SelectList(_context.Subs, "SubId", "SubId", line.LineId1);
            return View(line);
        }

        // POST: Lines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineId,LineNumber,LineBalance,LineMaxAmount,CreatedOn,ModifiedOn,LineId1")] Line line)
        {
            if (id != line.LineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(line);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineExists(line.LineId))
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
            ViewData["LineId1"] = new SelectList(_context.Subs, "SubId", "SubId", line.LineId1);
            return View(line);
        }

        // GET: Lines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lines == null)
            {
                return NotFound();
            }

            var line = await _context.Lines
                .Include(l => l.LineId1Navigation)
                .FirstOrDefaultAsync(m => m.LineId == id);
            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        // POST: Lines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lines == null)
            {
                return Problem("Entity set 'fawryContext.Lines'  is null.");
            }
            var line = await _context.Lines.FindAsync(id);
            if (line != null)
            {
                _context.Lines.Remove(line);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineExists(int id)
        {
          return (_context.Lines?.Any(e => e.LineId == id)).GetValueOrDefault();
        }
    }
}
