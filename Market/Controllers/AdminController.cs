using Market.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly MarketDbContext _context;

        public AdminController(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductsCount = await _context.Products.CountAsync();
            ViewBag.CategoriesCount = await _context.Categories.CountAsync();

            return View();
        }

        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            return View(categories);
        }
    }
}