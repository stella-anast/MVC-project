using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<ContentAdmin> ContentAdmins { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Provole> Provoles { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-07OD1NDP\\SQLEXPRESS;Database=CinemaApp;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ADMINS__3214EC27B602D848");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.Admins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ADMINS_user1");
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CINEMAS__3214EC27A7C0230A");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ContentAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CONTENT___3214EC274D3C078C");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.ContentAdmins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CONTENT_ADMIN_user1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC277D265927");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.UserUsernameNavigation).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CUSTOMERS_user1");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MOVIES__3214EC27ABD0C033");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ContentAdmin).WithMany(p => p.Movies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOVIES_CONTENT_ADMIN1");
        });

        modelBuilder.Entity<Provole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PROVOLES__3214EC27C2812CBE");

            entity.HasOne(d => d.Cinemas).WithMany(p => p.Provoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PROVOLES_CINEMAS1");

            entity.HasOne(d => d.ContentAdmin).WithMany(p => p.Provoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PROVOLES_CONTENT_ADMIN1");

            entity.HasOne(d => d.Movies).WithMany(p => p.Provoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PROVOLES_MOVIES1");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => new { e.CustomersId, e.ProvolesId }).HasName("PK__RESERVAT__8F0BFE19B083C913");

            entity.HasOne(d => d.Customers).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RESERVATIONS_CUSTOMERS1");

            entity.HasOne(d => d.Provoles).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RESERVATIONS_PROVOLES1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__user__F3DBC5734D8FA1AF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
