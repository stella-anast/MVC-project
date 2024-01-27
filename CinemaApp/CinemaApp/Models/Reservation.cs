using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[PrimaryKey("CustomersId", "ProvolesMoviesId")]
[Table("RESERVATIONS")]
public partial class Reservation
{
    [Column("NUMBER_OF_SEATS")]
    public int NumberOfSeats { get; set; }

    [Key]
    [Column("CUSTOMERS_ID")]
    public int CustomersId { get; set; }

    [Key]
    [Column("PROVOLES_MOVIES_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string ProvolesMoviesId { get; set; } = null!;

    [ForeignKey("CustomersId")]
    [InverseProperty("Reservations")]
    public virtual Customer Customers { get; set; } = null!;

    [ForeignKey("ProvolesMoviesId")]
    [InverseProperty("Reservations")]
    public virtual Provole ProvolesMovies { get; set; } = null!;
}
