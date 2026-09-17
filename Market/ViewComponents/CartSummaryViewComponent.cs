using Market.Data;
using Market.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Market.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly MarketDbContext _context;

        public CartSummaryViewComponent(
            MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int count;

            if (UserClaimsPrincipal.Identity?.IsAuthenticated == true)
            {
                var userId = UserClaimsPrincipal
                    .FindFirstValue(
                        ClaimTypes.NameIdentifier);

                count = _context.CartItems
                    .Where(item => item.UserId == userId)
                    .Sum(item => item.Quantity);
            }
            else
            {
                count = HttpContext.Session
                    .GetCart()
                    .Sum(item => item.Quantity);
            }

            return View(count);
        }
    }
}