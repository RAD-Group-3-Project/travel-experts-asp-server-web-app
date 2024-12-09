using TravelExpertData.Data;
using TravelExpertData.Models;

namespace TravelExpertData.Repository;
public static class TransactionRepository
{
    public static void AddTransaction(TravelExpertContext dbContext, Transaction transaction)
    {
        dbContext.Transactions.Add(transaction);
        dbContext.SaveChanges();
    }
}
