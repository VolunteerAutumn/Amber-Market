using Market.Models;

namespace Market.ViewModels
{
    public class CartItemViewModel
    {
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Total =>
            Product.Price * Quantity;
    }
}