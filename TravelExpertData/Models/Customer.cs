using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Index("AgentId", Name = "EmployeesCustomers")]
public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Display(Name ="First Name")]
    [StringLength(25)]
    public string CustFirstName { get; set; } = null!;
    [Display(Name = "Last Name")]
    [StringLength(25)]
    public string CustLastName { get; set; } = null!;

    [Display(Name = "Street Address")]
    [StringLength(75)]
    public string CustAddress { get; set; } = null!;

    [Display(Name = "City")]
    [StringLength(50)]
    public string CustCity { get; set; } = null!;

    [Display(Name = "Province")]
    [StringLength(2)]
    public string CustProv { get; set; } = null!;

    [Display(Name = "Postal Code")]
    [StringLength(7)]
    public string CustPostal { get; set; } = null!;

    [Display(Name = "Country")]
    [StringLength(25)]
    public string? CustCountry { get; set; }

    [Display(Name = "Home Phone")]
    [StringLength(20)]
    public string? CustHomePhone { get; set; }

    [Display(Name = "Business Phone")]
    [StringLength(20)]
    public string CustBusPhone { get; set; } = null!;
    
    [Display(Name = "Email")]
    [StringLength(50)]
    public string CustEmail { get; set; } = null!;

    [Display(Name = "Agent Representative")]
    public int? AgentId { get; set; }

    [Display(Name = "Travel Preferences")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Prefs { get; set; }
    [Display(Name = "Profile Image")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ProfileImg { get; set; }

    [ForeignKey("AgentId")]
    [InverseProperty("Customers")]
    public virtual Agent? Agent { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Customer")]
    public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    [Display(Name = "Rewards Cards")]
    [InverseProperty("Customer")]
    public virtual ICollection<CustomersReward> CustomersRewards { get; set; } = new List<CustomersReward>();
}
