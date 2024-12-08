using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public class CartItemRepository
{
    public static void AddCartItem(TravelExpertContext dbContext, CartItem cartItem)
    {
        dbContext.CartItems.Add(cartItem);
        dbContext.SaveChanges();
    }

    public static void RemoveCartItem(TravelExpertContext dbContext, int cartItemId)
    {
        var cartItem = dbContext.CartItems.Find(cartItemId);
        dbContext.CartItems.Remove(cartItem);
        dbContext.SaveChanges();
    }
}
