using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Controllers;
public class AccountController : Controller
{
    public IActionResult Login()
    {
        TempData["IsCustomBg"] = true;
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "password")
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Message = "Invalid username or password";
            return View();
        }
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Logout()
    {
        // TODO: Implement logout here
        return RedirectToAction("Index", "Home");
    }
}
