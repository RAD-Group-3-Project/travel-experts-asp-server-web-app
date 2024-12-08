namespace TravelExpertData.Models;
public class Transaction
{
    public int TransactionId { get; set; }
    public int WalletId { get; set; }
    public TransactionType TransactionType { get; set; }
    public Decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public Wallet Wallet { get; set; }
}

public enum TransactionType
{
    Credit,
    Debit,
}