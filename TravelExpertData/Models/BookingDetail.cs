using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Index("FeeId", Name = "Agency Fee Code")]
[Index("BookingId", Name = "BookingId")]
[Index("BookingId", Name = "BookingsBookingDetails")]
[Index("ClassId", Name = "ClassesBookingDetails")]
[Index("RegionId", Name = "Dest ID")]
[Index("RegionId", Name = "DestinationsBookingDetails")]
[Index("FeeId", Name = "FeesBookingDetails")]
[Index("ProductSupplierId", Name = "ProductSupplierId")]
[Index("ProductSupplierId", Name = "Products_SuppliersBookingDetails")]
public partial class BookingDetail
{
    [Key]
    public int BookingDetailId { get; set; }

    public double? ItineraryNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TripStart { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TripEnd { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(255)]
    public string? Destination { get; set; }

    [Column(TypeName = "money")]
    public decimal? BasePrice { get; set; }

    [Column(TypeName = "money")]
    public decimal? AgencyCommission { get; set; }

    public int? BookingId { get; set; }

    [StringLength(5)]
    public string? RegionId { get; set; }

    [StringLength(5)]
    public string? ClassId { get; set; }

    [StringLength(10)]
    public string? FeeId { get; set; }

    public int? ProductSupplierId { get; set; }

    [ForeignKey("BookingId")]
    [InverseProperty("BookingDetails")]
    public virtual Booking? Booking { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("BookingDetails")]
    public virtual Class? Class { get; set; }

    [ForeignKey("FeeId")]
    [InverseProperty("BookingDetails")]
    public virtual Fee? Fee { get; set; }

    [ForeignKey("ProductSupplierId")]
    [InverseProperty("BookingDetails")]
    public virtual ProductsSupplier? ProductSupplier { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("BookingDetails")]
    public virtual Region? Region { get; set; }
}
