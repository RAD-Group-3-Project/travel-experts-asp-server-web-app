using Microsoft.AspNetCore.Mvc;
using TravelExpertMVC.Data;
using TravelExpertMVC.Models;

namespace TravelExpertMVC.Controllers;
public class PackagesController : Controller
{
    TravelExpertContext _context;
    // GET: PackagesController
    public PackagesController(TravelExpertContext context)
    {
        _context = context;
    }

    // GET: PackageController
    public ActionResult Index()
    {
        var packages = new List<string> { "Beach Paradise", "Mountain Adventure", "City Lights" };
        return View(packages);
    }

    // GET: PackageController/Details/5
    public ActionResult Details(int id)
    {
        Package? package = PackagesManager.GetPackageById(_context, id);
        return View(package);
    }

    // GET: PackageController/Create
    public ActionResult Create()
    {
        Package package = new Package();
        return View(package);
    }

    // POST: PackageController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Package package)
    {
        try
        {
            if (ModelState.IsValid)
            {
                PackagesManager.AddPackage(_context, package);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(package);
            }
        }
        catch
        {
            return View(package);
        }
    }

    // GET: PackageController/Edit/5
    public ActionResult Edit(int id)
    {
        Package? package = PackagesManager.GetPackageById(_context, id);
        return View(package);
    }

    // POST: PackageController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Package package)
    {
        try
        {
            if (ModelState.IsValid)
            {
                PackagesManager.UpdatePackage(_context, id, package);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(package);
            }
        }
        catch
        {
            return View(package);
        }
    }

    // GET: PackageController/Delete/5
    [HttpGet]
    public ActionResult Delete(int id)
    {
        Package? package = PackagesManager.GetPackageById(_context, id);
        return View(package);
    }

    // POST: PackageController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            PackagesManager.DeletePackage(_context, id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
