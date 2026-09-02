using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly MarketDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CategoriesController(
            MarketDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // =========================
        // CREATE
        // =========================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Create folder for the new category
            CreateCategoryFolder(category.Name);

            return RedirectToAction(
                "Categories",
                "Admin");
        }

        // =========================
        // EDIT
        // =========================

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories
                .FindAsync(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Category category)
        {
            if (id != category.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            var existingCategory =
                await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
                return NotFound();

            var oldFolderName =
                GetCategoryFolderName(existingCategory.Name);

            var newFolderName =
                GetCategoryFolderName(category.Name);

            existingCategory.Name = category.Name;

            // Rename category folder if the name changed
            if (oldFolderName != newFolderName)
            {
                RenameCategoryFolder(
                    oldFolderName,
                    newFolderName);
            }
            else
            {
                CreateCategoryFolder(newFolderName);
            }

            await _context.SaveChangesAsync();

            // Update image paths of products in this category
            if (oldFolderName != newFolderName)
            {
                var products = await _context.Products
                    .Where(p => p.CategoryId == id)
                    .ToListAsync();

                foreach (var product in products)
                {
                    if (!string.IsNullOrEmpty(product.ImagePath))
                    {
                        product.ImagePath =
                            product.ImagePath.Replace(
                                $"/skins/{oldFolderName}/",
                                $"/skins/{newFolderName}/");
                    }
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(
                "Categories",
                "Admin");
        }

        // =========================
        // DELETE
        // =========================

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            var folderName =
                GetCategoryFolderName(category.Name);

            // Delete all product images
            foreach (var product in category.Products)
            {
                DeleteImage(product.ImagePath);
            }

            // Delete category folder
            DeleteCategoryFolder(folderName);

            // Delete category and its products
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Categories",
                "Admin");
        }

        // =========================
        // FOLDER HELPERS
        // =========================

        private void CreateCategoryFolder(string categoryName)
        {
            var folderName =
                GetCategoryFolderName(categoryName);

            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "skins",
                folderName);

            Directory.CreateDirectory(folderPath);
        }

        private void RenameCategoryFolder(
            string oldFolderName,
            string newFolderName)
        {
            var skinsPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "skins");

            var oldPath = Path.Combine(
                skinsPath,
                oldFolderName);

            var newPath = Path.Combine(
                skinsPath,
                newFolderName);

            if (Directory.Exists(oldPath))
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.Move(
                        oldPath,
                        newPath);
                }
            }
            else
            {
                Directory.CreateDirectory(newPath);
            }
        }

        private void DeleteCategoryFolder(
            string folderName)
        {
            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "images",
                "skins",
                folderName);

            if (Directory.Exists(folderPath))
            {
                Directory.Delete(
                    folderPath,
                    recursive: true);
            }
        }

        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            var relativePath =
                imagePath.TrimStart('/');

            var filePath = Path.Combine(
                _environment.WebRootPath,
                relativePath);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        private string GetCategoryFolderName(
            string categoryName)
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