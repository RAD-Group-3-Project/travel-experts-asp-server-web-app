using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Index("CustomerId", Name = "CustomersCreditCards")]
public partial class CreditCard
{
    [Key]
    public int CreditCardId { get; set; }

    [Column("CCName")]
    [StringLength(10)]
    public string Ccname { get; set; } = null!;

    [Column("CCNumber")]
    [StringLength(50)]
    public string Ccnumber { get; set; } = null!;

    [Column("CCExpiry", TypeName = "datetime")]
    public DateTime Ccexpiry { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CreditCards")]
    public virtual Customer Customer { get; set; } = null!;
}
