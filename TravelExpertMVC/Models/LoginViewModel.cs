using System.ComponentModel.DataAnnotations;

namespace TravelExpertMVC.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your username")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Please enter your password")]
    public string Password { get; set; }
}
