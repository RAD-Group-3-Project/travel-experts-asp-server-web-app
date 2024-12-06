using System.ComponentModel.DataAnnotations;
using TravelExpertData.Models;

namespace TravelExpertMVC.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your username")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Please enter your password")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
