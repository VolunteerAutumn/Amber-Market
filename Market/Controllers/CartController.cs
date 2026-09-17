using Market.Data;
using Market.Extensions;
using Market.Models;
using Market.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Market.Controllers
{
    public class CartController : Controller
    {
        private readonly MarketDbContext _context;

        public CartController(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                var cartItems = await _context.CartItems
                    .Include(item => item.Product)
                    .ThenInclude(product => product.Category)
                    .Where(item => item.UserId == userId)
                    .ToListAsync();

                var viewModel = new CartViewModel();

                foreach (var cartItem in cartItems)
                {
                    viewModel.Items.Add(
                        new CartItemViewModel
                        {
                            Product = cartItem.Product,
                            Quantity = cartItem.Quantity
                        });
                }

                viewModel.Total = viewModel.Items.Sum(
                    item => item.Total);

                return View(viewModel);
            }

            var sessionCart = HttpContext.Session.GetCart();

            var productIds = sessionCart
                .Select(item => item.ProductId)
                .ToList();

            var products = await _context.Products
                .Include(product => product.Category)
                .Where(product => productIds.Contains(product.Id))
                .ToListAsync();

            var sessionViewModel = new CartViewModel();

            foreach (var cartItem in sessionCart)
            {
                var product = products.FirstOrDefault(
                    product => product.Id == cartItem.ProductId);

                if (product == null)
                    continue;

                sessionViewModel.Items.Add(
                    new CartItemViewModel
                    {
                        Product = product,
                        Quantity = cartItem.Quantity
                    });
            }

            sessionViewModel.Total = sessionViewModel.Items.Sum(
                item => item.Total);

            return View(sessionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id)
        {
            var product = await _context.Products
                .FindAsync(id);

            if (product == null)
                return NotFound();

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                var existingItem = await _context.CartItems
                    .FirstOrDefaultAsync(item =>
                        item.UserId == userId &&
                        item.ProductId == id);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _context.CartItems.Add(
                        new CartItem
                        {
                            UserId = userId!,
                            ProductId = id,
                            Quantity = 1
                        });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var cart = HttpContext.Session.GetCart();

            var sessionItem = cart.FirstOrDefault(
                item => item.ProductId == id);

            if (sessionItem != null)
            {
                sessionItem.Quantity++;
            }
            else
            {
                cart.Add(
                    new SessionCartItem
                    {
                        ProductId = id,
                        Quantity = 1
                    });
            }

            HttpContext.Session.SetCart(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(
            int id,
            int change)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                var item = await _context.CartItems
                    .FirstOrDefaultAsync(cartItem =>
                        cartItem.UserId == userId &&
                        cartItem.ProductId == id);

                if (item != null)
                {
                    item.Quantity += change;

                    if (item.Quantity <= 0)
                    {
                        _context.CartItems.Remove(item);
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            var cart = HttpContext.Session.GetCart();

            var sessionItem = cart.FirstOrDefault(
                item => item.ProductId == id);

            if (sessionItem != null)
            {
                sessionItem.Quantity += change;

                if (sessionItem.Quantity <= 0)
                {
                    cart.Remove(sessionItem);
                }

                HttpContext.Session.SetCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                var item = await _context.CartItems
                    .FirstOrDefaultAsync(cartItem =>
                        cartItem.UserId == userId &&
                        cartItem.ProductId == id);

                if (item != null)
                {
                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            var cart = HttpContext.Session.GetCart();

            var sessionItem = cart.FirstOrDefault(
                item => item.ProductId == id);

            if (sessionItem != null)
            {
                cart.Remove(sessionItem);
                HttpContext.Session.SetCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                var items = await _context.CartItems
                    .Where(item => item.UserId == userId)
                    .ToListAsync();

                if (items.Any())
                {
                    _context.CartItems.RemoveRange(items);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            HttpContext.Session.SetCart(
                new List<SessionCartItem>());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

                var items = await _context.CartItems
                    .Where(item => item.UserId == userId)
                    .ToListAsync();

                if (items.Any())
                {
                    _context.CartItems.RemoveRange(items);

                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                HttpContext.Session.SetCart(
                    new List<SessionCartItem>());
            }

            return RedirectToAction(
                "Index",
                "Products");
        }
    }
}
