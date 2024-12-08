namespace TravelExpertData.Models;
public class Wallet
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
