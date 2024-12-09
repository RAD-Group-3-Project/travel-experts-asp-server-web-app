using TravelExpertData.Models;

namespace TravelExpertMVC.Models;

public class HomeViewModel
{
    public List<Package> Packages { get; set; }
    public List<Agency> Agencies { get; set; }
}
