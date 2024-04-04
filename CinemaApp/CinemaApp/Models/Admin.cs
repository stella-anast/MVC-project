using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

[Table("ADMINS")]
public partial class Admin
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

    [ForeignKey("UserUsername")]
    [InverseProperty("Admins")]
    public virtual User UserUsernameNavigation { get; set; } = null!;
}
