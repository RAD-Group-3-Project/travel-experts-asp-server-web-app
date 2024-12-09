using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Index("ProductId", Name = "ProductId")]
public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string ProdName { get; set; } = null!;

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ProductsSupplier> ProductsSuppliers { get; set; } = new List<ProductsSupplier>();
}
