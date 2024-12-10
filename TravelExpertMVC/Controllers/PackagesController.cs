using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    // For DB Stuff
    private readonly TravelExpertContext _context;

    // Replaces manager classes and DI's inlandcontext
    public PackagesController(SignInManager<User> signInManager, UserManager<User> userManager,
        TravelExpertContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
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
    public ActionResult ReviewBooking(int id)
    {

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

        // get pending cart
        Cart? cart = CartRepository.GetPendingCart(_context, (int)user.CustomerId);
        if (cart == null)
        {
            Debug.WriteLine($"Can't find pending cart in Payment() with customer id: {user.CustomerId}");
            TempData["ErrorMessage"] = ErrorMessages.GENERIC;
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

            TempData["BookingNo"] = newBooking.BookingNo;
            TempData["BookingDate"] = newBooking.BookingDate?.ToString("yyyy-M-d");
            TempData["TravelerCount"] = newBooking.TravelerCount.ToString();

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