using Microsoft.EntityFrameworkCore;
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

    public static List<CartItem> GetCartItems(TravelExpertContext dbContext, int cartId)
    {
        return dbContext.CartItems
            .Include(ci => ci.Package)
            .Where(ci => ci.CartId == cartId)
            .ToList();
    }
}
