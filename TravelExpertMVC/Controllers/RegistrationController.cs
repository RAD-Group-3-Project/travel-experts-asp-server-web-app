using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Controllers;
public class RegistrationController : Controller
{
    // GET: RegistrationController
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Submit(string name, string email)
    {
        ViewBag.Message = $"Thank you, {name}, for registering!";
        return View("Confirmation");
    }

    // GET: RegistrationController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: RegistrationController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: RegistrationController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: RegistrationController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: RegistrationController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: RegistrationController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: RegistrationController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
