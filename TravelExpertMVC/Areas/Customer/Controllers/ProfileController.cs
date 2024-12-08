using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;


namespace TravelExpertMVC.Areas.Customer.Controllers;
[Area("Customer")]
public class ProfileController : Controller
{
    // Identity object to manage the signin and file uploads via host
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    private readonly IWebHostEnvironment _host;
    // For DB Stufff
    TravelExpertContext _context;


    // Replaces manager classes and DI's inlandcontext
    public ProfileController(SignInManager<User> signInManager, UserManager<User> userManager, TravelExpertContext context, IWebHostEnvironment host)
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
    [HttpPost]
    public async Task<IActionResult> EditCustomerAsync(TravelExpertData.Models.Customer customer , IFormFile profileImage)
    {
        if (profileImage != null) 
        {
            var user = await userManager.GetUserAsync(User);
            int customerId = Convert.ToInt32(user.CustomerId);
            var extension = Path.GetExtension(profileImage.FileName);
            var filename = Path.Combine(_host.WebRootPath,"images", "profileImages", $"{customerId}{extension}");
            profileImage.CopyTo(new FileStream(filename, FileMode.Create));

            
        }
        return View();
    }
}
