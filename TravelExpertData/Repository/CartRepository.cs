using Microsoft.EntityFrameworkCore;
using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public class CartRepository
{
    public static Cart GetCart(TravelExpertContext dbContext, int customerId)
    {
        return dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Package)
            .FirstOrDefault(c => c.CustomerId == customerId);
    }

    public static void AddCart(TravelExpertContext dbContext, Cart cart)
    {
        dbContext.Carts.Add(cart);
        dbContext.SaveChanges();
    }

    public static void RemoveCart(TravelExpertContext dbContext, int cartId)
    {
        var cart = dbContext.Carts.Find(cartId);
        dbContext.Carts.Remove(cart);
        dbContext.SaveChanges();
    }
}
