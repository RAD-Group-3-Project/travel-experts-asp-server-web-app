using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[PrimaryKey("CustomerId", "RewardId")]
[Table("Customers_Rewards")]
[Index("CustomerId", Name = "CustomersCustomers_Rewards")]
[Index("RewardId", Name = "RewardsCustomers_Rewards")]
public partial class CustomersReward
{
    [Key]
    public int CustomerId { get; set; }

    [Key]
    public int RewardId { get; set; }

    [StringLength(25)]
    public string RwdNumber { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomersRewards")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("RewardId")]
    [InverseProperty("CustomersRewards")]
    public virtual Reward Reward { get; set; } = null!;
}
