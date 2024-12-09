using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using TravelExpertMVC.Models;

namespace TravelExpertMVC.Controllers;
public class HomeController : Controller
{
    private readonly TravelExpertContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;

    public HomeController(TravelExpertContext context, ILogger<HomeController> logger, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _context = context;
        _logger = logger;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        if (signInManager.IsSignedIn(User))
        {   
            
            var customer = new Customer();
            var user = await userManager.GetUserAsync(User);
            int? customerId = user?.CustomerId; 

            if (customerId.HasValue)  // Check if customerId has a value
            {
                customer = CustomerRepository.GetCustomerById(_context, customerId.Value);
            }
            else
            {
                // Handle the case where CustomerId is null (e.g., the user is not associated with a customer)
            }

            // Set the profile image or default image if not set
            if (!string.IsNullOrEmpty(customer.ProfileImg))
            {   

                // If there's a profile image, set the full path
                ViewBag.Image = "/images/profileImages/" + customer.ProfileImg;
            }
            else
            {   
                // Default image if no profile image is set
                ViewBag.Image = "/images/profileImages/default.jpg";
            }
        }
        List<Package> packages = PackagesRepository.GetPackages(_context);
        List<Agency> agencies = AgencyRepository.GetAgencies(_context);
        
        return View(new HomeViewModel() { Packages = packages, Agencies = agencies });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
