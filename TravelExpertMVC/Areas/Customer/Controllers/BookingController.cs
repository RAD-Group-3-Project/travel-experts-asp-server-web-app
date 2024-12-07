using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Areas.Customer.Controllers;

[Area("Customer")]
public class BookingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
