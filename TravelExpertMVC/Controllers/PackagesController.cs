﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using TravelExpertMVC.Areas.Customer.Controllers;
using TravelExpertMVC.Models;

namespace TravelExpertMVC.Controllers;
public class PackagesController : Controller
{
    // Identity object to manage the signin
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    // For DB Stufff
    TravelExpertContext _context;


    // Replaces manager classes and DI's inlandcontext
    public PackagesController(SignInManager<User> signInManager, UserManager<User> userManager, TravelExpertContext context)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        _context = context;

    }
    // Generate a random booking number 
    public static string GenerateRandomBooking()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        char[] stringChars = new char[6];

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
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
    [Authorize]
    public ActionResult ReviewBooking(int id)
    {   

        Package? package = PackagesRepository.GetPackageById(_context, id);
        BookingViewModel newBooking = new BookingViewModel()
        {
            PackageID = package.PackageId,
            PackageName = package.PkgName,
            StartDate = package.PkgStartDate,
            EndDate = package.PkgEndDate,
            Description = package.PkgDesc,
            BasePrice = package.PkgBasePrice,
            Travellers = 0,
            TripType ="A",

        };
        

        // Pass the list to the View using ViewBag
        ViewBag.List = GetTypes();
        

        return View(newBooking);
    }
    [HttpPost]
    public async Task<IActionResult> ReviewBookingAsync(BookingViewModel newBookingViewModel)
    {   
        string bookingNo = GenerateRandomBooking();
        var user = await userManager.GetUserAsync(User);
        if (ModelState.IsValid) // Checks our models validity
        {
            Booking newBooking = new Booking()
            {
                BookingDate = DateTime.Now,
                BookingNo = bookingNo,
                TravelerCount = newBookingViewModel.Travellers,
                CustomerId = user.CustomerId,
                TripTypeId = newBookingViewModel.TripType,
                PackageId = newBookingViewModel.PackageID
                


            }; 
            BookingRepository.AddBooking(_context, newBooking);
            return RedirectToAction("Index","Home");

        }
        //Reloads the page if the model is not valid
        ViewBag.List = GetTypes();
        return View();
    }
    private List<SelectListItem> GetTypes()
    {
        // get list of genres 
        List<TripType> types = TripTypeRepository.getAllTrips( _context);
        // create a select / dropdown list of genres 
        var list = new SelectList(types, "TripTypeId", "Ttname").ToList();
        // ensure that a genre is not slected first so as to see all trip types 
        list.Insert(0, new SelectListItem { Text = "Select your trip type!", Value = "A" , Disabled = true });
        // Returns the list
        return list;
    }


}
