using Microsoft.AspNetCore.Mvc;

namespace TravelExpertMVC.Controllers;
public class PackagesController : Controller
{
    // GET: PackageController
    public ActionResult Index()
    {
        var packages = new List<string> { "Beach Paradise", "Mountain Adventure", "City Lights" };
        return View(packages);
    }

    // GET: PackageController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: PackageController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: PackageController/Create
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

    // GET: PackageController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: PackageController/Edit/5
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

    // GET: PackageController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: PackageController/Delete/5
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
