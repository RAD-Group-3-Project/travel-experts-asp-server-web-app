using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
