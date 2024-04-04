using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("MOVIES")]
public partial class Movie
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(45)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("LENGTH")]
    public int Length { get; set; }

    [Column("TYPE")]
    [StringLength(45)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("SUMMARY", TypeName = "text")]
    public string Summary { get; set; } = null!;

    [Column("DIRECTOR")]
    [StringLength(45)]
    [Unicode(false)]
    public string Director { get; set; } = null!;

    [Column("RELEASE_YEAR")]
    public int ReleaseYear { get; set; }

    [Column("CONTENT_ADMIN_ID")]
    public int ContentAdminId { get; set; }

    [ForeignKey("ContentAdminId")]
    [InverseProperty("Movies")]
    public virtual ContentAdmin ContentAdmin { get; set; } = null!;

    [InverseProperty("Movies")]
    public virtual ICollection<Provole> Provoles { get; set; } = new List<Provole>();
}
