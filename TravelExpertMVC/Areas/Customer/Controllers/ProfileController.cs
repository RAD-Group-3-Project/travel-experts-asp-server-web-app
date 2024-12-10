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
            ModelState.AddModelError("", "Cannot find customer");
        }
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
            ModelState.AddModelError("", "Cannot find customer");
        }
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

        return View(customer);
    }
    [HttpPost]
    [RequestSizeLimit(5000000)] // 5 meg upload limit
    public async Task<IActionResult> EditCustomerAsync(TravelExpertData.Models.Customer customer , IFormFile profileImage)
    {
        if (profileImage != null)
        {
            
            var user = await userManager.GetUserAsync(User);
            int customerId = Convert.ToInt32(user.CustomerId);
            var extension = Path.GetExtension(profileImage.FileName);
            var permittedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };
            var filename = Path.Combine(_host.WebRootPath, "images", "profileImages", $"{customerId}{extension}");
            
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Invalid file type.");
            }
            // Check if the customer already has a profile image and delete it if it exists
            if (!string.IsNullOrEmpty(customer.ProfileImg))
            {
                var existingImagePath = Path.Combine(_host.WebRootPath, "images", "profileImages", customer.ProfileImg);
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath); 
                }
            }

            // Save the new profile image
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                await profileImage.CopyToAsync(fileStream); 
            }

            customer.ProfileImg = $"{customerId}{extension}";
        }

        // Passes the new image to the viewbag with a "cachebuster" for immediate viewing 
        ViewBag.Image = $"/images/profileImages/{customer.ProfileImg}?t={DateTime.Now.Ticks}";

        // Update the customer in the database
        await CustomerRepository.UpdateCustomerAsync(_context, customer);

        return View();

        
    }
}
