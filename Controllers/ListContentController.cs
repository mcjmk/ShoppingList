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
    public class ListContentController : Controller
    {
        private readonly AppDbContext _context;

        public ListContentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ListContent
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ListContent.Include(l => l.Product).Include(l => l.UserShoppingList);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ListContent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListContent == null)
            {
                return NotFound();
            }

            var listContent = await _context.ListContent
                .Include(l => l.Product)
                .Include(l => l.UserShoppingList)
                .FirstOrDefaultAsync(m => m.ListContentId == id);
            if (listContent == null)
            {
                return NotFound();
            }

            return View(listContent);
        }

        // GET: ListContent/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct");
            ViewData["ShoppingListId"] = new SelectList(_context.UserShoppingList, "ShoppingListId", "ShoppingListName");
            return View();
        }

        // POST: ListContent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListContentId,ShoppingListId,ProductId,Quantity")] ListContent listContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct", listContent.ProductId);
            ViewData["ShoppingListId"] = new SelectList(_context.UserShoppingList, "ShoppingListId", "ShoppingListName", listContent.ShoppingListId);
            return View(listContent);
        }

        // GET: ListContent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListContent == null)
            {
                return NotFound();
            }

            var listContent = await _context.ListContent.FindAsync(id);
            if (listContent == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct", listContent.ProductId);
            ViewData["ShoppingListId"] = new SelectList(_context.UserShoppingList, "ShoppingListId", "ShoppingListName", listContent.ShoppingListId);
            return View(listContent);
        }

        // POST: ListContent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListContentId,ShoppingListId,ProductId,Quantity")] ListContent listContent)
        {
            if (id != listContent.ListContentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListContentExists(listContent.ListContentId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "NameProduct", listContent.ProductId);
            ViewData["ShoppingListId"] = new SelectList(_context.UserShoppingList, "ShoppingListId", "ShoppingListName", listContent.ShoppingListId);
            return View(listContent);
        }

        // GET: ListContent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListContent == null)
            {
                return NotFound();
            }

            var listContent = await _context.ListContent
                .Include(l => l.Product)
                .Include(l => l.UserShoppingList)
                .FirstOrDefaultAsync(m => m.ListContentId == id);
            if (listContent == null)
            {
                return NotFound();
            }

            return View(listContent);
        }

        // POST: ListContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListContent == null)
            {
                return Problem("Entity set 'AppDbContext.ListContent'  is null.");
            }
            var listContent = await _context.ListContent.FindAsync(id);
            if (listContent != null)
            {
                _context.ListContent.Remove(listContent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListContentExists(int id)
        {
          return (_context.ListContent?.Any(e => e.ListContentId == id)).GetValueOrDefault();
        }
    }
}
