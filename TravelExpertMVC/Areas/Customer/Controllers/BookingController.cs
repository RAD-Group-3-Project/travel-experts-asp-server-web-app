using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;

namespace TravelExpertMVC.Areas.Customer.Controllers;

[Area("Customer")]
public class BookingController : Controller
{
    // Identity object to manage the signin
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    // For DB Stufff
    TravelExpertContext _context;


    // Replaces manager classes and DI's inlandcontext
    public BookingController(SignInManager<User> signInManager, UserManager<User> userManager, TravelExpertContext context)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        _context = context;

    }
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
        int? customerId = user.CustomerId;
        List<Booking> bookingsById = new List<Booking>();

        
        if (customerId.HasValue)
        {
            // Call the method with the actual value of customerId
            bookingsById = BookingRepository.getBookingsById(_context, customerId.Value);
        }
        else
        {
            // Handle the case where customerId is null (you can throw an exception, return an empty list, etc.)
            bookingsById = new List<Booking>(); // Or handle the case appropriately
        }
        if (signInManager.IsSignedIn(User))
        {
            var customer = new TravelExpertData.Models.Customer();
           
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
        //ViewBag.List = bookingsById;
        return View(bookingsById);
    }
}
