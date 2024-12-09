using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

public partial class Agent
{
    [Key]
    public int AgentId { get; set; }

    [StringLength(20)]
    public string? AgtFirstName { get; set; }

    [StringLength(5)]
    public string? AgtMiddleInitial { get; set; }

    [StringLength(20)]
    public string? AgtLastName { get; set; }

    [StringLength(20)]
    public string? AgtBusPhone { get; set; }

    [StringLength(50)]
    public string? AgtEmail { get; set; }

    [StringLength(20)]
    public string? AgtPosition { get; set; }

    public int? AgencyId { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [ForeignKey("AgencyId")]
    [InverseProperty("Agents")]
    public virtual Agency? Agency { get; set; }

    [InverseProperty("Agent")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
