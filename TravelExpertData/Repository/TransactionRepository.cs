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

    public static List<Transaction> GetTransactions(TravelExpertContext dbContext, Guid walletId)
    {
        return dbContext.Transactions.Where(t => t.WalletId == walletId).ToList();
    }
}
