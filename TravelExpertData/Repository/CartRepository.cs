using Microsoft.EntityFrameworkCore;
using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public class CartRepository
{
    public static Cart? GetCartById(TravelExpertContext dbContext, int cartId)
    {
        return dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Package)
            .FirstOrDefault(c => c.Id == cartId);
    }

    public static Cart? GetCartByCustomerId(TravelExpertContext dbContext, int customerId)
    {
        return dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Package)
            .FirstOrDefault(c => c.CustomerId == customerId);
    }

    public static void AddOrUpdateCart(TravelExpertContext dbContext, Cart cart)
    {
        var existingCart = dbContext.Carts.Find(cart.Id);
        if (existingCart == null)
        {
            dbContext.Carts.Add(cart);
        }
        else
        {
            dbContext.Carts.Update(cart);
        }
        dbContext.SaveChanges();
    }

    public static void RemoveCart(TravelExpertContext dbContext, int cartId)
    {
        var cart = dbContext.Carts.Find(cartId);
        dbContext.Carts.Remove(cart);
        dbContext.SaveChanges();
    }

    public static Cart? GetPendingCart(TravelExpertContext dbContext, int customerId)
    {
        return dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Package)
            .FirstOrDefault(c => c.CustomerId == customerId && c.Status == CartStatus.Pending);
    }

    public static void UpdateCart(TravelExpertContext dbContext, Cart cart)
    {
        dbContext.Carts.Update(cart);
        dbContext.SaveChanges();
    }
}
