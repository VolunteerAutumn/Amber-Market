using System.Text.Json;
using Market.Models;

namespace Market.Extensions
{
    public static class SessionExtensions
    {
        private const string CartKey = "Cart";

        public static List<SessionCartItem> GetCart(
            this ISession session)
        {
            var cartJson = session.GetString(CartKey);

            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<SessionCartItem>();
            }

            return JsonSerializer.Deserialize<List<SessionCartItem>>(
                       cartJson)
                   ?? new List<SessionCartItem>();
        }

        public static void SetCart(
            this ISession session,
            List<SessionCartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);

            session.SetString(CartKey, cartJson);
        }
    }
}