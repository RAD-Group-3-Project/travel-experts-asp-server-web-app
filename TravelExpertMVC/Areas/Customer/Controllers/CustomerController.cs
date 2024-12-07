using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Areas.Customer.Controllers;
public class CustomerController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult Wallet()
    {
        return View();
    }

    public IActionResult Bookings()
    {
        return View();
    }
}
