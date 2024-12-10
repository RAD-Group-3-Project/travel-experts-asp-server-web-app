using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using TravelExpertMVC.Areas.Customer.Models;
using TravelExpertMVC.Models;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class WalletController : Controller
{
    public const string COUPON_1000 = "COUPON-1000-REDEEM";
    public const string COUPON_50000 = "COUPON-5000-REDEEM";
    public const string COUPON_10000 = "COUPON-10000-REDEEM";

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

            TempData["ErrorMessage"] = "Can't find wallet. Please contact support.";

            return View(new MyWalletViewModel() { Transactions = [], Wallet = new Wallet() });
        }

        // get transaction by wallet id
        var transactions = TransactionRepository.GetTransactions(_context, wallet.Id);

        return View(new MyWalletViewModel() { Transactions = transactions, Wallet = wallet });
    }

    [HttpPost]
    public async Task<IActionResult> Redeem(string couponCode)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var wallet = WalletRepository.GetWallet(_context, (int)user.CustomerId!);
        if (wallet == null)
        {
            Debug.WriteLine($"Error: can't find wallet in Redeem() by customer id: [{user.CustomerId}]");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        decimal couponAmount = 0;
        switch (couponCode)
        {
            case COUPON_1000:
                couponAmount = 1000;
                break;
            case COUPON_50000:
                couponAmount = 5000;
                break;
            case COUPON_10000:
                couponAmount = 10000;
                break;
            default:
                Debug.WriteLine($"Error: unknown coupon code: [{couponCode}]");
                TempData["ErrorMessage"] = "Invalid coupon code. Please enter a valid coupon code.";

                return RedirectToAction("Index");
        }

        wallet.Balance += couponAmount;

        // update balance
        WalletRepository.UpdateWallet(_context, wallet);

        // create transaction
        Transaction transaction = new Transaction()
        {
            WalletId = wallet.Id,
            Description = $"Top-up money by coupon",
            Amount = couponAmount,
            TransactionDate = DateTime.Now,
            TransactionType = TransactionType.Credit
        };
        TransactionRepository.AddTransaction(_context, transaction);

        TempData["SuccessMessage"] = "Coupon redeemed successfully!";

        return RedirectToAction("Index");
    }
}