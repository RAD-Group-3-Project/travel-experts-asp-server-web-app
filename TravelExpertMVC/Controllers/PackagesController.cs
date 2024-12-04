using Microsoft.AspNetCore.Mvc;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;

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
        List<Package> packages = PackagesRepository.GetPackages(_context);
        return View(packages);
    }

    // GET: PackageController/Details/5
    public ActionResult Details(int id)
    {
        Package? package = PackagesRepository.GetPackageById(_context, id);
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
                PackagesRepository.AddPackage(_context, package);
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
        Package? package = PackagesRepository.GetPackageById(_context, id);
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
                PackagesRepository.UpdatePackage(_context, id, package);
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
        Package? package = PackagesRepository.GetPackageById(_context, id);
        return View(package);
    }

    // POST: PackageController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            PackagesRepository.DeletePackage(_context, id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
