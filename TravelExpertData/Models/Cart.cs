namespace TravelExpertData.Models;
public class Cart
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public List<CartItem> CartItems { get; set; }
    public CartStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}

public enum CartStatus
{
    Cancelled,
    Completed,
    Pending
}