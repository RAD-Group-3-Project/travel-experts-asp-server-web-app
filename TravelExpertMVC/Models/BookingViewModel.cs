using System.ComponentModel.DataAnnotations;

namespace TravelExpertMVC.Models
{
    public class BookingViewModel
    {
        [Key]
        [Display(Name = "Package ID")]
        public int PackageID { get; set; }

        [Required(ErrorMessage = "Package Name is required")]
        [Display(Name = "Package Name")]
        [StringLength(50, ErrorMessage = "Package Name cannot exceed 50 characters")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        [CustomValidation(typeof(BookingViewModel), nameof(ValidateEndDate))]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(255, ErrorMessage = "Exceeded the max character limit")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Base Price is required")]
        [Display(Name = "Base Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base Price must be a positive value")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "Number of Travellers is required")]
        [Display(Name = "Number of Travellers")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Travellers must be at least 1")]
        public int? Travellers { get; set; }

        [Required(ErrorMessage = "Trip Type is required")]
        [Display(Name = "Trip Type")]
        public string? TripType {  get; set; }

        /// <summary>
        /// Custom validation method for ensuring End Date is after the Start Date.
        /// </summary>
        /// <param name="endDate"></param>
        /// <param name="context"></param>
        /// <returns>Returns the result after the input of the start and end date</returns>
        public static ValidationResult? ValidateEndDate(DateTime? endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as BookingViewModel;

            if (instance?.StartDate == null || endDate == null)
            {
                return ValidationResult.Success;
            }

            if (endDate < instance.StartDate)
            {
                return new ValidationResult("End Date must be afer the Start Date");
            }

            return ValidationResult.Success;
        }
    }
}
