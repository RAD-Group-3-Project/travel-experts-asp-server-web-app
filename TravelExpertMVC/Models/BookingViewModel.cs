namespace TravelExpertMVC.Models
{
    public class BookingViewModel
    {
        public int PackageID { get; set; }
        public string PackageName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int? Travellers { get; set; }
        public string? TripType {  get; set; }



    }
}
