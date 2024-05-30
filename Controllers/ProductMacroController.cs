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
    public class ProductMacroController : Controller
    {
        private readonly AppDbContext _context;

        public ProductMacroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductMacro
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductMacro.Include(p => p.Macro).Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductMacro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductMacro == null)
            {
                return NotFound();
            }

            var productMacro = await _context.ProductMacro
                .Include(p => p.Macro)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductMacroId == id);
            if (productMacro == null)
            {
                return NotFound();
            }

            return View(productMacro);
        }

        // GET: ProductMacro/Create
        public IActionResult Create()
        {
            ViewData["MacroId"] = new SelectList(_context.Macro, "MacroID", "MacroID");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct");
            return View();
        }

        // POST: ProductMacro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductMacroId,ProductId,MacroId,Value")] ProductMacro productMacro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productMacro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MacroId"] = new SelectList(_context.Macro, "MacroID", "MacroID", productMacro.MacroId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct", productMacro.ProductId);
            return View(productMacro);
        }

        // GET: ProductMacro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductMacro == null)
            {
                return NotFound();
            }

            var productMacro = await _context.ProductMacro.FindAsync(id);
            if (productMacro == null)
            {
                return NotFound();
            }
            ViewData["MacroId"] = new SelectList(_context.Macro, "MacroID", "MacroID", productMacro.MacroId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct", productMacro.ProductId);
            return View(productMacro);
        }

        // POST: ProductMacro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductMacroId,ProductId,MacroId,Value")] ProductMacro productMacro)
        {
            if (id != productMacro.ProductMacroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productMacro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductMacroExists(productMacro.ProductMacroId))
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
            ViewData["MacroId"] = new SelectList(_context.Macro, "MacroID", "MacroID", productMacro.MacroId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct", productMacro.ProductId);
            return View(productMacro);
        }

        // GET: ProductMacro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductMacro == null)
            {
                return NotFound();
            }

            var productMacro = await _context.ProductMacro
                .Include(p => p.Macro)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductMacroId == id);
            if (productMacro == null)
            {
                return NotFound();
            }

            return View(productMacro);
        }

        // POST: ProductMacro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductMacro == null)
            {
                return Problem("Entity set 'AppDbContext.ProductMacro'  is null.");
            }
            var productMacro = await _context.ProductMacro.FindAsync(id);
            if (productMacro != null)
            {
                _context.ProductMacro.Remove(productMacro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductMacroExists(int id)
        {
          return (_context.ProductMacro?.Any(e => e.ProductMacroId == id)).GetValueOrDefault();
        }
    }
}
