using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Index("SupplierId", Name = "SupplierId")]
public partial class Supplier
{
    [Key]
    public int SupplierId { get; set; }

    [StringLength(255)]
    public string? SupName { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<ProductsSupplier> ProductsSuppliers { get; set; } = new List<ProductsSupplier>();

    [InverseProperty("Supplier")]
    public virtual ICollection<SupplierContact> SupplierContacts { get; set; } = new List<SupplierContact>();
}
