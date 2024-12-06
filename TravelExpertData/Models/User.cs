using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertData.Models;

[Keyless]
public partial class User
{
    [Column("userid")]
    public int Userid { get; set; }

    [Column("user_login")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserLogin { get; set; } = null!;

    [Column("user_password")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserPassword { get; set; } = null!;

    [Column("is_admin")]
    public bool IsAdmin { get; set; }
}
