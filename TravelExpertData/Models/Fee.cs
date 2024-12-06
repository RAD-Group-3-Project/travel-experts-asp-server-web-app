using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

public partial class Fee
{
    [Key]
    [StringLength(10)]
    public string FeeId { get; set; } = null!;

    [StringLength(50)]
    public string FeeName { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal FeeAmt { get; set; }

    [StringLength(50)]
    public string? FeeDesc { get; set; }

    [InverseProperty("Fee")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
