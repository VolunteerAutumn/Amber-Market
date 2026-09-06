using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MarketDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(
            MarketDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index(string? sort)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            ViewBag.Sort = sort;

            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // CREATE
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Product product,
            IFormFile? ImageFile)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();

            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Please select an image.");
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var category = await _context.Categories
                .FindAsync(product.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Selected category does not exist.");

                return View(product);
            }

            // Create category folder
            var categoryFolder = GetCategoryFolderName(category.Name);

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "skins",
                categoryFolder);

            Directory.CreateDirectory(folderPath);

            // Generate unique filename
            var extension = Path.GetExtension(ImageFile!.FileName);

            var fileName = Guid.NewGuid().ToString() + extension;

            var filePath = Path.Combine(
                folderPath,
                fileName);

            // Save image
            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            // Path stored in database
            product.ImagePath =
                $"/images/skins/{categoryFolder}/{fileName}";

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Products",
                "Admin");
        }

        // =========================
        // EDIT
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.Categories =
                await _context.Categories.ToListAsync();

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Product product,
            IFormFile? ImageFile)
        {
            if (id != product.Id)
                return NotFound();

            ViewBag.Categories =
                await _context.Categories.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var existingProduct =
                await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
                return NotFound();

            var category = await _context.Categories
                .FindAsync(product.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Selected category does not exist.");

                return View(product);
            }

            // Update normal product fields
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Condition = product.Condition;
            existingProduct.Rarity = product.Rarity;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Rating = product.Rating;

            // If a new image was uploaded
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Delete old image
                DeleteImage(existingProduct.ImagePath);

                // Create category folder
                var categoryFolder =
                    GetCategoryFolderName(category.Name);

                var folderPath = Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    "skins",
                    categoryFolder);

                Directory.CreateDirectory(folderPath);

                // Generate new filename
                var extension =
                    Path.GetExtension(ImageFile.FileName);

                var fileName =
                    Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(
                    folderPath,
                    fileName);

                // Save new image
                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Update database path
                existingProduct.ImagePath =
                    $"/images/skins/{categoryFolder}/{fileName}";
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Products",
                "Admin");
        }

        // =========================
        // DELETE
        // =========================

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            // Delete physical image
            DeleteImage(product.ImagePath);

            // Delete database record
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Products",
                "Admin");
        }

        // =========================
        // HELPERS
        // =========================

        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var relativePath = imagePath.TrimStart('/');

            var filePath = Path.Combine(
                _environment.WebRootPath,
                relativePath);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        private string GetCategoryFolderName(string categoryName)
        {
            var invalidChars =
                Path.GetInvalidFileNameChars();

            var result = new string(
                categoryName
                    .ToLowerInvariant()
                    .Select(c =>
                        invalidChars.Contains(c)
                            ? '_'
                            : c)
                    .ToArray());

            result = result.Replace(" ", "_");

            return result;
        }
    }
}