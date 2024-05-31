using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data;
using ShoppingList.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingList.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly AppDbContext _context;

        public ShoppingListController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var username = HttpContext.User.Identity.Name;

            
            Debug.WriteLine($"Username: {username}");

            if (string.IsNullOrEmpty(username))
            {
                
                Debug.WriteLine($"Redirecting to login due to missing username. User authenticated: {User.Identity.IsAuthenticated}");
                return RedirectToAction("Login", "Account");
            }

            
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                Debug.WriteLine($"User not found in database. Username: {username}");
                return RedirectToAction("Login", "Account");
            }

            
            Debug.WriteLine($"UserId: {user.UserId}");

            
            var shoppingLists = await _context.UserShoppingList
                .Where(sl => sl.UserId == user.UserId)
                .ToListAsync();

            return View(shoppingLists);
        }

        public IActionResult Details(int id)
        {
            var shoppingList = _context.UserShoppingList
                .Include(sl => sl.ListContents)
                .ThenInclude(lc => lc.Product)
                .FirstOrDefault(sl => sl.ShoppingListId == id);

            if (shoppingList == null)
            {
                return NotFound();
            }

            return View(shoppingList);
        }

        public IActionResult Sort(int id, string sortOrder)
        {
            var shoppingList = _context.UserShoppingList
                .Include(sl => sl.ListContents)
                .ThenInclude(lc => lc.Product)
                .FirstOrDefault(sl => sl.ShoppingListId == id);

            if (shoppingList == null)
            {
                return NotFound();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    shoppingList.ListContents = shoppingList.ListContents.OrderByDescending(lc => lc.Product.NameProduct).ToList();
                    break;
                case "name_asc":
                default:
                    shoppingList.ListContents = shoppingList.ListContents.OrderBy(lc => lc.Product.NameProduct).ToList();
                    break;
            }

            return View("Details", shoppingList);
        }
    // GET: ShoppingList/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ShoppingList/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ShoppingListName")] UserShoppingList userShoppingList)
    {
        if (ModelState.IsValid)
        {
            var username = HttpContext.User.Identity.Name;

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            userShoppingList.UserId = user.UserId;

            _context.Add(userShoppingList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(userShoppingList);
    }

    // POST: ShoppingList/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userShoppingList = await _context.UserShoppingList.FindAsync(id);
        if (userShoppingList != null)
        {
            _context.UserShoppingList.Remove(userShoppingList);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    // GET: ShoppingList/AddProduct/5
    public IActionResult AddProduct(int id)
    {
        var model = new ListContent { ShoppingListId = id };
        ViewBag.Products = new SelectList(_context.Product, "ProductId", "NameProduct");
        return View(model);
    }

    // POST: ShoppingList/AddProduct
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddProduct(ListContent listContent)
    {
        if (ModelState.IsValid)
        {
            _context.ListContent.Add(listContent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = listContent.ShoppingListId });
        }
        ViewBag.Products = new SelectList(_context.Product, "ProductId", "NameProduct", listContent.ProductId);
        return View(listContent);
    }

    // POST: ShoppingList/DeleteProduct/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var listContent = await _context.ListContent.FindAsync(id);
        if (listContent != null)
        {
            _context.ListContent.Remove(listContent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = listContent.ShoppingListId });
        }
        return NotFound();
    }
    }
}