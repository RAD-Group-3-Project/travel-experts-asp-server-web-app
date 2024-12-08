namespace TravelExpertData.Models;
public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public virtual Cart Cart { get; set; }
    public int PackageId { get; set; }
    public virtual Package Package { get; set; }
    public int Traveller { get; set; }
    public int TripTypeId { get; set; }
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}
