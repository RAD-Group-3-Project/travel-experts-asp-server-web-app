using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TravelExpertData.Data;
using TravelExpertData.Models;
using TravelExpertData.Repository;
using TravelExpertMVC.Models;
using TravelExpertMVC.Util;

namespace TravelExpertMVC.Controllers;

public class PackagesController : Controller
{
    // Identity object to manage the signin
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IEmailSender _emailsender;


    // For DB Stuff
    private readonly TravelExpertContext _context;

    // Replaces manager classes and DI's inlandcontext
    public PackagesController(SignInManager<User> signInManager, UserManager<User> userManager,
        TravelExpertContext context, IEmailSender emailSender)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
        _emailsender = emailSender;
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

    [Authorize]
    public async Task<ActionResult> ReviewBooking(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var customer = new TravelExpertData.Models.Customer();

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
            TripType = "A",
        };

        // Pass the list to the View using ViewBag
        ViewBag.List = GetTypes();

        return View(newBooking);
    }

    [HttpPost]
    public async Task<IActionResult> ReviewBookingAsync(BookingViewModel newBookingViewModel)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var customerId = Convert.ToInt32(user.CustomerId);
        var customer = CustomerRepository.GetCustomerById(_context, customerId);

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
        if (ModelState.IsValid) // Checks our models validity
        {
            // find the pending cart for the user
            Cart? pendingCart = CartRepository.GetPendingCart(_context, (int)user.CustomerId);
            Cart cart;
            if (pendingCart != null)
            {
                // use the pending cart
                cart = pendingCart;
            }
            else
            {
                // create a new cart
                cart = new Cart()
                {
                    CustomerId = (int)user.CustomerId,
                    Status = CartStatus.Pending,
                    CreatedAt = DateTime.Now,
                };
            }

            CartItem cartItem = new CartItem()
            {
                Cart = cart,
                PackageId = newBookingViewModel.PackageID,
                Traveller = (int)newBookingViewModel.Travellers,
                TripTypeId = newBookingViewModel.TripType,
                Price = newBookingViewModel.BasePrice,
            };

            cart.SubTotal += cartItem.Price;
            cart.Tax = CalculateGST(cart.SubTotal);
            cart.Total = cart.SubTotal + cart.Tax;
            CartRepository.AddOrUpdateCart(_context, cart);
            CartItemRepository.AddCartItem(_context, cartItem);

            return RedirectToAction("Payment", "Packages");
        }

        //Reloads the page if the model is not valid
        ViewBag.List = GetTypes();
        return View();
    }

    private List<SelectListItem> GetTypes()
    {
        // get list of genres 
        List<TripType> types = TripTypeRepository.getAllTrips(_context);
        // create a select / dropdown list of genres 
        var list = new SelectList(types, "TripTypeId", "Ttname").ToList();
        // ensure that a genre is not slected first so as to see all trip types 
        list.Insert(0, new SelectListItem { Text = "Select your trip type!", Value = "A", Disabled = true });
        // Returns the list
        return list;
    }

    [Authorize]
    public async Task<IActionResult> Payment()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var customerId = Convert.ToInt32(user.CustomerId);
        var customer = CustomerRepository.GetCustomerById(_context, customerId);

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

        // get pending cart
        Cart? cart = CartRepository.GetPendingCart(_context, (int)user.CustomerId);
        if (cart == null)
        {
            return View(new PaymentViewModel() { Cart = new Cart(), CartItems = new List<CartItem>() });
        }

        // get CartItem detail
        List<CartItem> cartItems = CartItemRepository.GetCartItems(_context, cart.Id);

        return View(new PaymentViewModel() { Cart = cart, CartItems = cartItems });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PaymentAsync(int CardId)
    {
        // Send email details 
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // get Cart detail
        Cart? cart = CartRepository.GetPendingCart(_context, (int)user.CustomerId);
        if (cart == null)
        {
            Debug.WriteLine($"Can't find pending cart in PaymentAsync() with customer id: {user.CustomerId}");
            TempData["ErrorMessage"] = ErrorMessages.GENERIC;
            return RedirectToAction("Payment", "Packages");
        }

        // get CartItem detail
        List<CartItem> cartItems = CartItemRepository.GetCartItems(_context, cart.Id);

        // is sufficient fund?
        Wallet? wallet = WalletRepository.GetWallet(_context, (int)user.CustomerId);
        if (wallet == null || wallet.Balance < cart.Total)
        {
            TempData["ErrorMessage"] = ErrorMessages.INSUFFICIENT_FUND;
            return RedirectToAction("Payment", "Packages");
        }

        // deduct the amount from the wallet
        wallet.Balance -= cart.Total;
        WalletRepository.UpdateWallet(_context, wallet);

        // create transaction
        Transaction newTransaction = new Transaction()
        {
            WalletId = wallet.Id,
            TransactionType = TransactionType.Debit,
            Amount = cart.Total,
            Description = "Payment for booking",
            TransactionDate = DateTime.Now,
        };
        TransactionRepository.AddTransaction(_context, newTransaction);

        List<string> bookingNoList = [];
        List<string> bookingDateList = [];
        List<string> bookingNameList = [];


        // create booking
        foreach (CartItem item in cartItems)
        {
            // Create a new booking
            Booking newBooking = new Booking()
            {
                BookingDate = DateTime.Now,
                BookingNo = GenerateRandomBooking(),
                TravelerCount = item.Traveller,
                CustomerId = user.CustomerId,
                TripTypeId = item.TripTypeId,
                PackageId = item.PackageId
            };
            BookingRepository.AddBooking(_context, newBooking);

            bookingNoList.Add(newBooking.BookingNo);
            bookingDateList.Add(newBooking.BookingDate?.ToString("yyyy-M-d")!);
            bookingNameList.Add(newBooking.Package.PkgName);

            // find the product_supplier_id
            List<PackagesProductsSupplier> ppsList = PackageProductSupplierRepository.GetPackagesProductsSupplierByPackageId(_context, item.PackageId);

            foreach (PackagesProductsSupplier pps in ppsList)
            {
                if (pps.IsActive == true)
                {
                    // Create booking Detail
                    BookingDetail newBookingDetail = new BookingDetail()
                    {
                        ItineraryNo = cartItems.IndexOf(item) + 1,
                        TripStart = item.Package.PkgStartDate,
                        TripEnd = item.Package.PkgEndDate,
                        Description = item.Package.PkgDesc,
                        BasePrice = item.Price,
                        AgencyCommission = 0,
                        BookingId = newBooking.BookingId,
                        ProductSupplierId = pps.ProductSupplierId,
                    };
                    BookingDetailRepository.AddBookingDetail(_context, newBookingDetail);
                }
            }

            // Update the cart status
            cart.Status = CartStatus.Completed;
            CartRepository.UpdateCart(_context, cart);
        }

        TempData["BookingNoList"] = bookingNoList;
        TempData["BookingDateList"] = bookingDateList;
        var customer = CustomerRepository.GetCustomerById(_context, (int)user.CustomerId);
        var emailbookings = String.Join(",", bookingNoList);
        var emailbookingdates = String.Join(",", bookingDateList);
        var locations = String.Join(",", bookingNameList);
        // sending the email with booking no and date to user
        try
        {
            await _emailsender.SendEmailAsync(
                customer.CustEmail,
                $"Congratulations {customer.CustFirstName}! You are BOOKED!",
                $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Flight Booking Confirmation</title>
                        <style>
                            body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
                            .container {{ background-color: #ffffff; padding: 20px; margin: 20px auto; width: 80%; max-width: 600px; border-radius: 10px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); }}
                            h1 {{ color: #007bff; font-size: 28px; text-align: center; }}
                            p {{ font-size: 18px; color: #333; line-height: 1.6; }}
                            .booking-details {{ font-size: 16px; margin: 20px 0; }}
                            .booking-details span {{ font-weight: bold; color: #007bff; }}
                            .footer {{ text-align: center; margin-top: 20px; font-size: 14px; color: #999; }}
                            .highlight {{ font-size: 20px; font-weight: bold; color: #ff5722; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Flight Booking Confirmation</h1>
                            <p>Dear {customer.CustFirstName} {customer.CustLastName},</p>
                            <p>We’re excited to confirm your upcoming flights! You’re booked on the following flights:</p>
        
                            <div class='booking-details'>
                                <p><span>Booking Numbers:</span> <span class='highlight'>{emailbookings}</span></p>
                                <p><span>Locations:</span> <span class='highlight'>{locations}</span></p>
                                <p><span>Dates:</span> <span class='highlight'>{emailbookingdates}</span></p>
                            </div>

                            <p>See you soon on your journey. Safe travels!</p>

                            <div class='footer'>
                                <p>If you have any questions, feel free to reach out to us.</p>
                                <p>&copy; Travel Experts</p>
                            </div>
                        </div>
                    </body>
                    </html>"
            );
        }
        catch (Exception ex)
        {
            // Log the exception and handle the error (e.g., notify the user or retry)
            //_logger.LogError(ex, "Error sending email.");
        }



        return RedirectToAction("ThankYou");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RemoveItemAsync(int cartItemId, int cartId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        CartItemRepository.RemoveCartItem(_context, cartItemId);

        // update subtotal, tax, and total
        Cart? cart = CartRepository.GetCartById(_context, cartId);
        if (cart == null)
        {
            Debug.WriteLine($"Can't find cart with cart id: {cartId}");
            return RedirectToAction("Index", "Home");
        }

        cart.SubTotal = cart.CartItems.Sum(ci => ci.Price);
        cart.Tax = CalculateGST(cart.SubTotal);
        cart.Total = cart.SubTotal + cart.Tax;
        CartRepository.UpdateCart(_context, cart);

        return RedirectToAction("Payment", "Packages");
    }

    private decimal CalculateGST(decimal basePrice)
    {
        const decimal GST_RATE = 0.05m;
        return basePrice * GST_RATE;
    }

    public IActionResult ThankYou()
    {
        return View();
    }
}