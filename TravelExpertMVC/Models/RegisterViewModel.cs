using System.ComponentModel.DataAnnotations;

namespace TravelExpertMVC.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        [Display(Name = "Province")]
        public string? Prov { get; set; }
        [Display(Name = "Postal Code")]
        public string? Postal { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Home Phone")]
        public string? HomePhone { get; set; }
        [Display(Name = "Business Phone")]
        public string? BusPhone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
