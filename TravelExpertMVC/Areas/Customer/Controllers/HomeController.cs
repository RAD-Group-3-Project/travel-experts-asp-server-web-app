using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
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
            var customer = new TravelExpertData.Models.Customer();
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
                ViewBag.Image = $"/images/profileImages/{customer.ProfileImg}?t={DateTime.Now.Ticks}";
            }
            else
            {
                // Default image if no profile image is set
                ViewBag.Image = "/images/profileImages/default.jpg";
            }
        }

        // Dashboard
        return View();
    }
}
