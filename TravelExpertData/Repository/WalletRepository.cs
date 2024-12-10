using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public static class WalletRepository
{
    public static Wallet CreateWallet(TravelExpertContext dbContext, int customerId)
    {
        Wallet wallet = new Wallet()
        {
            CustomerId = customerId,
            Balance = 0,
            LastUpdated = DateTime.Now
        };
        dbContext.Wallets.Add(wallet);
        dbContext.SaveChanges();
        return wallet;
    }
    public static Wallet? GetWallet(TravelExpertContext dbContext, int customerId)
    {
        return dbContext.Wallets.FirstOrDefault(w => w.CustomerId == customerId);
    }

    public static void UpdateWallet(TravelExpertContext dbContext, Wallet wallet)
    {
        dbContext.Wallets.Update(wallet);
        dbContext.SaveChanges();
    }
}
