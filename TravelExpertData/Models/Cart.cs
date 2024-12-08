namespace TravelExpertData.Models;
public class Cart
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public List<CartItem> CartItems { get; set; }
    public DateTime CreatedAt { get; set; }
}
