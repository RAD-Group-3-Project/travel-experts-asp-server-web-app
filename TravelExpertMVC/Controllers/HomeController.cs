using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public HomeController(TravelExpertContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Package> packages = PackagesRepository.GetPackages(_context);

        // TODO: Implement the GetAgency method in the AgenciesRepository class
        List<Agency> agencies = _context.Agencies.Include(a => a.Agents).ToList();

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
