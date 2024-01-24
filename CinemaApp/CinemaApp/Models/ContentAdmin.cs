﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("CONTENT_ADMIN")]
public partial class ContentAdmin
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

    [InverseProperty("ContentAdmin")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    [InverseProperty("ContentAdmin")]
    public virtual ICollection<Provole> Provoles { get; set; } = new List<Provole>();

    [ForeignKey("UserUsername")]
    [InverseProperty("ContentAdmins")]
    public virtual User UserUsernameNavigation { get; set; } = null!;
}
