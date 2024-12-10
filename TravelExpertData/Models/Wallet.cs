using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExpertData.Models;
public class Wallet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public int CustomerId { get; set; }
    public Decimal Balance { get; set; }
    public DateTime LastUpdated { get; set; }
    public Customer Customer { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
