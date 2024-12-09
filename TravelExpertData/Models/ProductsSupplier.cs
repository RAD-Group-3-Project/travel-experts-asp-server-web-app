using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Table("Products_Suppliers")]
[Index("SupplierId", Name = "Product Supplier ID")]
[Index("ProductId", Name = "ProductId")]
[Index("ProductSupplierId", Name = "ProductSupplierId")]
[Index("ProductId", Name = "ProductsProducts_Suppliers1")]
[Index("SupplierId", Name = "SuppliersProducts_Suppliers1")]
public partial class ProductsSupplier
{
    [Key]
    public int ProductSupplierId { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    [InverseProperty("ProductSupplier")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [InverseProperty("ProductSupplier")]
    public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; } = new List<PackagesProductsSupplier>();

    [ForeignKey("ProductId")]
    [InverseProperty("ProductsSuppliers")]
    public virtual Product? Product { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("ProductsSuppliers")]
    public virtual Supplier? Supplier { get; set; }
}
