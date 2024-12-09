using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

public partial class Affiliation
{
    [Key]
    [StringLength(10)]
    public string AffilitationId { get; set; } = null!;

    [StringLength(50)]
    public string? AffName { get; set; }

    [StringLength(50)]
    public string? AffDesc { get; set; }

    [InverseProperty("Affiliation")]
    public virtual ICollection<SupplierContact> SupplierContacts { get; set; } = new List<SupplierContact>();
}
