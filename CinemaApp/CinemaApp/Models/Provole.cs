using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("PROVOLES")]
public partial class Provole
{
    [Key]
    [Column("ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [Column("MOVIES_TIME", TypeName = "datetime")]
    public DateTime MoviesTime { get; set; }

    [Column("CINEMAS_ID")]
    public int CinemasId { get; set; }

    [Column("MOVIES_ID")]
    public int MoviesId { get; set; }

    [Column("CONTENT_ADMIN_ID")]
    public int ContentAdminId { get; set; }

    [ForeignKey("CinemasId")]
    [InverseProperty("Provoles")]
    public virtual Cinema Cinemas { get; set; } = null!;

    [ForeignKey("ContentAdminId")]
    [InverseProperty("Provoles")]
    public virtual ContentAdmin ContentAdmin { get; set; } = null!;

    [ForeignKey("MoviesId")]
    [InverseProperty("Provoles")]
    public virtual Movie Movies { get; set; } = null!;

    [InverseProperty("ProvolesMovies")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
