using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[PrimaryKey("CustomersId", "ProvolesId")]
[Table("RESERVATIONS")]
public partial class Reservation
{
    [Column("NUMBER_OF_SEATS")]
    public int NumberOfSeats { get; set; }

    [Key]
    [Column("CUSTOMERS_ID")]
    public int CustomersId { get; set; }

    [Key]
    [Column("PROVOLES_ID")]
    [StringLength(45)]
    [Unicode(false)]
    public string ProvolesId { get; set; } = null!;

    [ForeignKey("CustomersId")]
    [InverseProperty("Reservations")]
    public virtual Customer Customers { get; set; } = null!;

    [ForeignKey("ProvolesId")]
    [InverseProperty("Reservations")]
    public virtual Provole Provoles { get; set; } = null!;
}
