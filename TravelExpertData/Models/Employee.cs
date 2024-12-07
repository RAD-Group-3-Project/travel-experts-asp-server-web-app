using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Keyless]
public partial class Employee
{
    [StringLength(20)]
    public string EmpFirstName { get; set; } = null!;

    [StringLength(5)]
    public string? EmpMiddleInitial { get; set; }

    [StringLength(20)]
    public string EmpLastName { get; set; } = null!;

    [StringLength(20)]
    public string EmpBusPhone { get; set; } = null!;

    [StringLength(50)]
    public string EmpEmail { get; set; } = null!;

    [StringLength(20)]
    public string EmpPosition { get; set; } = null!;
}
