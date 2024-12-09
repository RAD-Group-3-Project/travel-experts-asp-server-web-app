using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class WalletController : Controller
{   
    // Identity object to manage the signin and file uploads via host
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    private readonly IWebHostEnvironment _host;
    // For DB Stufff
    TravelExpertContext _context;


    // Replaces manager classes and DI's inlandcontext
    public WalletController(SignInManager<User> signInManager, UserManager<User> userManager, TravelExpertContext context, IWebHostEnvironment host)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        _context = context;
        _host = host;

    }
    public async Task<IActionResult> Index()
    {
        var customer = new TravelExpertData.Models.Customer();
        var user = await userManager.GetUserAsync(User);
        int? customerId = user?.CustomerId;

        if (customerId.HasValue)  // Check if customerId has a value
        {
            customer = CustomerRepository.GetCustomerById(_context, customerId.Value);
        }
        else
        {
        }

        // Set the profile image or default image if not set
        if (!string.IsNullOrEmpty(customer.ProfileImg))
        {

            ViewBag.Image = $"/images/profileImages/{customer.ProfileImg}?t={DateTime.Now.Ticks}";
        }
        else
        {
            // Default image 
            ViewBag.Image = "/images/profileImages/default.jpg";
        }
    
        return View();
    }
}
