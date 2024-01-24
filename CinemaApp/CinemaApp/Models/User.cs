using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("user")]
public partial class User
{
    [Key]
    [Column("username")]
    [StringLength(32)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(32)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(256)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("salt")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Salt { get; set; }

    [Column("role")]
    [StringLength(45)]
    [Unicode(false)]
    public string? Role { get; set; }

    [InverseProperty("UserUsernameNavigation")]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [InverseProperty("UserUsernameNavigation")]
    public virtual ICollection<ContentAdmin> ContentAdmins { get; set; } = new List<ContentAdmin>();

    [InverseProperty("UserUsernameNavigation")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
