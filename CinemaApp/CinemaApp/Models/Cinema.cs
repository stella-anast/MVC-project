using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("CINEMAS")]
public partial class Cinema
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("SEATS")]
    public int Seats { get; set; } 

    [Column("3D")]
    [StringLength(45)]
    [Unicode(false)]
    public string _3d { get; set; } = null!;

    [InverseProperty("Cinemas")]
    public virtual ICollection<Provole> Provoles { get; set; } = new List<Provole>();
}
