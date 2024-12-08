using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;

namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class ProfileController : Controller
{
    // Identity object to manage the signin
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    // For DB Stufff
    TravelExpertContext _context;


    // Replaces manager classes and DI's inlandcontext
    public ProfileController(SignInManager<User> signInManager, UserManager<User> userManager, TravelExpertContext context)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        _context = context;

    }
    public async Task<IActionResult> Index()
    {
        var customer = new TravelExpertData.Models.Customer();
        var user = await userManager.GetUserAsync(User);
        int customerId = Convert.ToInt32(user.CustomerId);
        if (customerId != null)
        {
           customer = CustomerRepository.GetCustomerById(_context, customerId);
        }
        else
        {
            
        }
        return View(customer);
    }

    public async Task<IActionResult> EditCustomer(int id) 
    {
        var customer = new TravelExpertData.Models.Customer();
        var user = await userManager.GetUserAsync(User);
        int customerId = id;
        if (customerId != null)
        {
            customer = CustomerRepository.GetCustomerById(_context, customerId);
        }
        else
        {

        }
        return View(customer);
    }
}
