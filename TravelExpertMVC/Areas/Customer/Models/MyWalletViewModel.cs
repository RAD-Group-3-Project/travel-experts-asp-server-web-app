using TravelExpertData.Models;

namespace TravelExpertMVC.Areas.Customer.Models;

public class MyWalletViewModel
{
    public Wallet Wallet { get; set; }
    public List<Transaction> Transactions { get; set; }
}
