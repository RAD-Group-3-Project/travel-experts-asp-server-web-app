using Microsoft.AspNetCore.Identity;

namespace TravelExpertData.Models
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public int? CustomerId { get; set; }
        public Customer customers { get; set; }
    }

}
