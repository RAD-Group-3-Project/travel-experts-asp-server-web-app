using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Dashboard
        return View();
    }
}
