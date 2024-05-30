using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Models;

namespace ShoppingList.Controllers
{
    public class MacroController : Controller
    {
        private readonly AppDbContext _context;

        public MacroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Macro
        public async Task<IActionResult> Index()
        {
              return _context.Macro != null ? 
                          View(await _context.Macro.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Macro'  is null.");
        }

        // GET: Macro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Macro == null)
            {
                return NotFound();
            }

            var macro = await _context.Macro
                .FirstOrDefaultAsync(m => m.MacroID == id);
            if (macro == null)
            {
                return NotFound();
            }

            return View(macro);
        }

        // GET: Macro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Macro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MacroID,MacroName")] Macro macro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(macro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(macro);
        }

        // GET: Macro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Macro == null)
            {
                return NotFound();
            }

            var macro = await _context.Macro.FindAsync(id);
            if (macro == null)
            {
                return NotFound();
            }
            return View(macro);
        }

        // POST: Macro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MacroID,MacroName")] Macro macro)
        {
            if (id != macro.MacroID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(macro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MacroExists(macro.MacroID))
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
            return View(macro);
        }

        // GET: Macro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Macro == null)
            {
                return NotFound();
            }

            var macro = await _context.Macro
                .FirstOrDefaultAsync(m => m.MacroID == id);
            if (macro == null)
            {
                return NotFound();
            }

            return View(macro);
        }

        // POST: Macro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Macro == null)
            {
                return Problem("Entity set 'AppDbContext.Macro'  is null.");
            }
            var macro = await _context.Macro.FindAsync(id);
            if (macro != null)
            {
                _context.Macro.Remove(macro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MacroExists(int id)
        {
          return (_context.Macro?.Any(e => e.MacroID == id)).GetValueOrDefault();
        }
    }
}
