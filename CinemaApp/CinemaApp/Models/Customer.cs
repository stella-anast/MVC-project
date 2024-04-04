using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("CUSTOMERS")]
public partial class Customer
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("user_username")]
    [StringLength(32)]
    [Unicode(false)]
    public string UserUsername { get; set; } = null!;

    [InverseProperty("Customers")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [ForeignKey("UserUsername")]
    [InverseProperty("Customers")]
    public virtual User UserUsernameNavigation { get; set; } = null!;
}
