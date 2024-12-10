using TravelExpertData.Models;

namespace TravelExpertMVC.Models;

public class PaymentViewModel
{
    public Cart Cart { get; set; }
    public List<CartItem> CartItems { get; set; }
}
