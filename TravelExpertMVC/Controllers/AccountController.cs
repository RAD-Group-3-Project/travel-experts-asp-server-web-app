﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using TravelExpertMVC.Models;
using TravelExpertMVC.Util;

namespace TravelExpertMVC.Controllers;
public class AccountController : Controller
{
    // Identity object to manage the signin
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    // For DB Stufff
    TravelExpertContext _context;


    // Replaces manager classes and DI's inlandcontext
    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, TravelExpertContext context)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        _context = context;

    }

    public IActionResult Login()
    {
        TempData["IsCustomBg"] = true;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel loginModel)
    {
        if (ModelState.IsValid) // Checks our models validity
        {
            // Authenticates signin 
            var result = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, false);
            // If successful redirect to home 
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            // Otherwise reload the page
            else
            {
                ModelState.AddModelError("", "Invalid Login");
                return View();
            }
        }
        return View();
    }

    public IActionResult Register()
    {
        TempData["IsCustomBg"] = true;

        ViewBag.Provinces = new SelectList(Utils.Provinces);
        ViewBag.Cities = new SelectList(Utils.Cities);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel newRegistration)
    {

        TempData["IsCustomBg"] = true;

        ViewBag.Provinces = new SelectList(Utils.Provinces);
        ViewBag.Cities = new SelectList(Utils.Cities);

        if (ModelState.IsValid)
        {
            Customer newCustomer = new Customer()
            {
                CustFirstName = newRegistration.FirstName,
                CustLastName = newRegistration.LastName,
                CustAddress = newRegistration.Address,
                CustCity = newRegistration.City,
                CustProv = newRegistration.Prov,
                CustPostal = newRegistration.Postal,
                CustCountry = newRegistration.Country,
                CustHomePhone = newRegistration.HomePhone,
                CustBusPhone = newRegistration.BusPhone,
                CustEmail = newRegistration.Email
            };
            CustomerRepository.AddCustomer(_context, newCustomer);

            int custId = CustomerRepository.GetLastId(_context);

            // Create a wallet for the new customer
            WalletRepository.CreateWallet(_context, custId);

            User newUser = new User()
            {
                UserName = newRegistration.Email,
                Email = newRegistration.Email,
                PhoneNumber = newRegistration.HomePhone,
                CustomerId = custId

            };
            var userResult = await userManager.CreateAsync(newUser, newRegistration.Password);

            if (userResult.Succeeded)
            {
                await signInManager.SignInAsync(newUser, false);
                return RedirectToAction("Index", "Home");

            }
            foreach (var item in userResult.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

        }
        return View();
    }

    public async Task<IActionResult> LogoutAsync()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
