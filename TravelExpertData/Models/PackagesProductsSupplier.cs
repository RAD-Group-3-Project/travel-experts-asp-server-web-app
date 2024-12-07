using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Table("Packages_Products_Suppliers")]
[Index("PackageId", Name = "PackagesPackages_Products_Suppliers")]
[Index("ProductSupplierId", Name = "ProductSupplierId")]
[Index("ProductSupplierId", Name = "Products_SuppliersPackages_Products_Suppliers")]
[Index("PackageId", "ProductSupplierId", Name = "UQ__Packages__29CA8E9590D3A63F", IsUnique = true)]
public partial class PackagesProductsSupplier
{
    [Key]
    public int PackageProductSupplierId { get; set; }

    public int PackageId { get; set; }

    public int ProductSupplierId { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [ForeignKey("PackageId")]
    [InverseProperty("PackagesProductsSuppliers")]
    public virtual Package Package { get; set; } = null!;

    [ForeignKey("ProductSupplierId")]
    [InverseProperty("PackagesProductsSuppliers")]
    public virtual ProductsSupplier ProductSupplier { get; set; } = null!;
}
