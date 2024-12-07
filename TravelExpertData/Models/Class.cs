using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

public partial class Class
{
    [Key]
    [StringLength(5)]
    public string ClassId { get; set; } = null!;

    [StringLength(20)]
    public string ClassName { get; set; } = null!;

    [StringLength(50)]
    public string? ClassDesc { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
