using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using TravelExpertMVC.Areas.Customer.Models;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class WalletController : Controller
{
    private readonly TravelExpertContext _context;
    private readonly UserManager<User> _userManager;

    public WalletController(TravelExpertContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // get wallet by customer id
        var wallet = WalletRepository.GetWallet(_context, (int)user.CustomerId!);
        if (wallet == null)
        {
            Debug.WriteLine($"Error: can't find wallet by customer id: [{user.CustomerId}]");
            return View(new MyWalletViewModel() { Transactions = [], Wallet = new Wallet() });
        }

        // get transaction by wallet id
        var transactions = TransactionRepository.GetTransactions(_context, wallet.Id);

        return View(new MyWalletViewModel() { Transactions = transactions, Wallet = wallet });
    }
}
