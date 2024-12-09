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
    public async Task<IActionResult> EditCustomerAsync(TravelExpertData.Models.Customer customer , IFormFile profileImage)
    {
        if (profileImage != null)
        {
            var user = await userManager.GetUserAsync(User);
            int customerId = Convert.ToInt32(user.CustomerId);
            var extension = Path.GetExtension(profileImage.FileName);
            var filename = Path.Combine(_host.WebRootPath, "images", "profileImages", $"{customerId}{extension}");

            // Check if the customer already has a profile image and delete it if it exists
            if (!string.IsNullOrEmpty(customer.ProfileImg))
            {
                var existingImagePath = Path.Combine(_host.WebRootPath, "images", "profileImages", customer.ProfileImg);
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath); // Delete the existing file
                }
            }

            // Save the new profile image
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                await profileImage.CopyToAsync(fileStream); // Asynchronously copy the file to disk
            }

            customer.ProfileImg = $"{customerId}{extension}"; // Update the profile image reference in the model
        }

        // Pass the image URL to the view and refresh the cached image
        ViewBag.Image = $"/images/profileImages/{customer.ProfileImg}?t={DateTime.Now.Ticks}";

        // Update the customer in the database
        await CustomerRepository.UpdateCustomerAsync(_context, customer);

        return View();

        
    }
}
