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
    public class UserShoppingListController : Controller
    {
        private readonly AppDbContext _context;

        public UserShoppingListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserShoppingList
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserShoppingList.Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserShoppingList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserShoppingList == null)
            {
                return NotFound();
            }

            var userShoppingList = await _context.UserShoppingList
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ShoppingListId == id);
            if (userShoppingList == null)
            {
                return NotFound();
            }

            return View(userShoppingList);
        }

        // GET: UserShoppingList/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: UserShoppingList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingListId,ShoppingListName,UserId")] UserShoppingList userShoppingList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userShoppingList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userShoppingList.UserId);
            return View(userShoppingList);
        }

        // GET: UserShoppingList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserShoppingList == null)
            {
                return NotFound();
            }

            var userShoppingList = await _context.UserShoppingList.FindAsync(id);
            if (userShoppingList == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userShoppingList.UserId);
            return View(userShoppingList);
        }

        // POST: UserShoppingList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingListId,ShoppingListName,UserId")] UserShoppingList userShoppingList)
        {
            if (id != userShoppingList.ShoppingListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userShoppingList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserShoppingListExists(userShoppingList.ShoppingListId))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userShoppingList.UserId);
            return View(userShoppingList);
        }

        // GET: UserShoppingList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserShoppingList == null)
            {
                return NotFound();
            }

            var userShoppingList = await _context.UserShoppingList
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ShoppingListId == id);
            if (userShoppingList == null)
            {
                return NotFound();
            }

            return View(userShoppingList);
        }

        // POST: UserShoppingList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserShoppingList == null)
            {
                return Problem("Entity set 'AppDbContext.UserShoppingList'  is null.");
            }
            var userShoppingList = await _context.UserShoppingList.FindAsync(id);
            if (userShoppingList != null)
            {
                _context.UserShoppingList.Remove(userShoppingList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserShoppingListExists(int id)
        {
          return (_context.UserShoppingList?.Any(e => e.ShoppingListId == id)).GetValueOrDefault();
        }
    }
}
