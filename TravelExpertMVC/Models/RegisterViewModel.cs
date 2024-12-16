using System.ComponentModel.DataAnnotations;

namespace TravelExpertMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(25)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(25)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(75)]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [Display(Name = "Province")]
        [StringLength(2)]
        public string? Prov { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [Display(Name = "Postal Code")]
        [StringLength(7, ErrorMessage = "Postal code characters cannot exceed 7 characters")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code format")]
        public string? Postal { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        [StringLength(25, ErrorMessage = "Country characters cannot exceed 25 characters")]
        public string? Country { get; set; }

        [Display(Name = "Home Phone")]
        [StringLength(20, ErrorMessage = "Home Phone number character cannot exceed 20 characters")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid phone number format")]
        public string? HomePhone { get; set; }

        [Required(ErrorMessage = "Business Phone number is required")]
        [Display(Name = "Business Phone")]
        [StringLength(20, ErrorMessage = "Business Phone number characters cannot exceed 20 characters")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid phone number format")]
        public string? BusPhone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [StringLength(50, ErrorMessage = "Email characters cannot exceed 50 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password must match")]
        public string? ConfirmPassword { get; set; }
    }
}
