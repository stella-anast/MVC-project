﻿// <auto-generated />
using System;
using CinemaApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApp.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240130215927_CreateNew")]
    partial class CreateNew
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaApp.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("NAME");

                    b.Property<string>("UserUsername")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("user_username");

                    b.HasKey("Id")
                        .HasName("PK__ADMINS__3214EC270C94B5CB");

                    b.HasIndex("UserUsername");

                    b.ToTable("ADMINS");
                });

            modelBuilder.Entity("CinemaApp.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("NAME");

                    b.Property<int>("Seats")
                        .HasColumnType("int")
                        .HasColumnName("SEATS");

                    b.Property<string>("_3d")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("3D");

                    b.HasKey("Id")
                        .HasName("PK__CINEMAS__3214EC27084EA0B1");

                    b.ToTable("CINEMAS");
                });

            modelBuilder.Entity("CinemaApp.Models.ContentAdmin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("NAME");

                    b.Property<string>("UserUsername")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("user_username");

                    b.HasKey("Id")
                        .HasName("PK__CONTENT___3214EC2748F8FDF9");

                    b.HasIndex("UserUsername");

                    b.ToTable("CONTENT_ADMIN");
                });

            modelBuilder.Entity("CinemaApp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("NAME");

                    b.Property<string>("UserUsername")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("user_username");

                    b.HasKey("Id")
                        .HasName("PK__CUSTOMER__3214EC27A8CA8BE2");

                    b.HasIndex("UserUsername");

                    b.ToTable("CUSTOMERS");
                });

            modelBuilder.Entity("CinemaApp.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContentAdminId")
                        .HasColumnType("int")
                        .HasColumnName("CONTENT_ADMIN_ID");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("DIRECTOR");

                    b.Property<int>("Length")
                        .HasColumnType("int")
                        .HasColumnName("LENGTH");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("NAME");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int")
                        .HasColumnName("RELEASE_YEAR");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("SUMMARY");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("TYPE");

                    b.HasKey("Id")
                        .HasName("PK__MOVIES__3214EC275C8030F5");

                    b.HasIndex("ContentAdminId");

                    b.ToTable("MOVIES");
                });

            modelBuilder.Entity("CinemaApp.Models.Provole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemasId")
                        .HasColumnType("int")
                        .HasColumnName("CINEMAS_ID");

                    b.Property<int>("ContentAdminId")
                        .HasColumnType("int")
                        .HasColumnName("CONTENT_ADMIN_ID");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int")
                        .HasColumnName("MOVIES_ID");

                    b.Property<DateTime>("MoviesTime")
                        .HasColumnType("datetime")
                        .HasColumnName("MOVIES_TIME");

                    b.Property<int>("Seats")
                        .HasColumnType("int")
                        .HasColumnName("SEATS");

                    b.HasKey("Id")
                        .HasName("PK__PROVOLES__3214EC27968945E6");

                    b.HasIndex("CinemasId");

                    b.HasIndex("ContentAdminId");

                    b.HasIndex("MoviesId");

                    b.ToTable("PROVOLES");
                });

            modelBuilder.Entity("CinemaApp.Models.Reservation", b =>
                {
                    b.Property<int>("CustomersId")
                        .HasColumnType("int")
                        .HasColumnName("CUSTOMERS_ID");

                    b.Property<int>("ProvolesMoviesId")
                        .HasColumnType("int")
                        .HasColumnName("PROVOLES_MOVIES_ID");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int")
                        .HasColumnName("NUMBER_OF_SEATS");

                    b.HasKey("CustomersId", "ProvolesMoviesId")
                        .HasName("PK__RESERVAT__8F0BFE192D3A3743");

                    b.HasIndex("ProvolesMoviesId");

                    b.ToTable("RESERVATIONS");
                });

            modelBuilder.Entity("CinemaApp.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("username");

                    b.Property<string>("Email")
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("role");

                    b.Property<string>("Salt")
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("salt");

                    b.HasKey("Username")
                        .HasName("PK__user__F3DBC57389683771");

                    b.ToTable("user");
                });

            modelBuilder.Entity("CinemaApp.Models.Admin", b =>
                {
                    b.HasOne("CinemaApp.Models.User", "UserUsernameNavigation")
                        .WithMany("Admins")
                        .HasForeignKey("UserUsername")
                        .IsRequired()
                        .HasConstraintName("fk_ADMINS_user1");

                    b.Navigation("UserUsernameNavigation");
                });

            modelBuilder.Entity("CinemaApp.Models.ContentAdmin", b =>
                {
                    b.HasOne("CinemaApp.Models.User", "UserUsernameNavigation")
                        .WithMany("ContentAdmins")
                        .HasForeignKey("UserUsername")
                        .IsRequired()
                        .HasConstraintName("fk_CONTENT_ADMIN_user1");

                    b.Navigation("UserUsernameNavigation");
                });

            modelBuilder.Entity("CinemaApp.Models.Customer", b =>
                {
                    b.HasOne("CinemaApp.Models.User", "UserUsernameNavigation")
                        .WithMany("Customers")
                        .HasForeignKey("UserUsername")
                        .IsRequired()
                        .HasConstraintName("fk_CUSTOMERS_user1");

                    b.Navigation("UserUsernameNavigation");
                });

            modelBuilder.Entity("CinemaApp.Models.Movie", b =>
                {
                    b.HasOne("CinemaApp.Models.ContentAdmin", "ContentAdmin")
                        .WithMany("Movies")
                        .HasForeignKey("ContentAdminId")
                        .IsRequired()
                        .HasConstraintName("FK_MOVIES_CONTENT_ADMIN1");

                    b.Navigation("ContentAdmin");
                });

            modelBuilder.Entity("CinemaApp.Models.Provole", b =>
                {
                    b.HasOne("CinemaApp.Models.Cinema", "Cinemas")
                        .WithMany("Provoles")
                        .HasForeignKey("CinemasId")
                        .IsRequired()
                        .HasConstraintName("fk_PROVOLES_CINEMAS1");

                    b.HasOne("CinemaApp.Models.ContentAdmin", "ContentAdmin")
                        .WithMany("Provoles")
                        .HasForeignKey("ContentAdminId")
                        .IsRequired()
                        .HasConstraintName("fk_PROVOLES_CONTENT_ADMIN1");

                    b.HasOne("CinemaApp.Models.Movie", "Movies")
                        .WithMany("Provoles")
                        .HasForeignKey("MoviesId")
                        .IsRequired()
                        .HasConstraintName("fk_PROVOLES_MOVIES1");

                    b.Navigation("Cinemas");

                    b.Navigation("ContentAdmin");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("CinemaApp.Models.Reservation", b =>
                {
                    b.HasOne("CinemaApp.Models.Customer", "Customers")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomersId")
                        .IsRequired()
                        .HasConstraintName("fk_RESERVATIONS_CUSTOMERS1");

                    b.HasOne("CinemaApp.Models.Provole", "ProvolesMovies")
                        .WithMany("Reservations")
                        .HasForeignKey("ProvolesMoviesId")
                        .IsRequired()
                        .HasConstraintName("fk_RESERVATIONS_PROVOLES1");

                    b.Navigation("Customers");

                    b.Navigation("ProvolesMovies");
                });

            modelBuilder.Entity("CinemaApp.Models.Cinema", b =>
                {
                    b.Navigation("Provoles");
                });

            modelBuilder.Entity("CinemaApp.Models.ContentAdmin", b =>
                {
                    b.Navigation("Movies");

                    b.Navigation("Provoles");
                });

            modelBuilder.Entity("CinemaApp.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("CinemaApp.Models.Movie", b =>
                {
                    b.Navigation("Provoles");
                });

            modelBuilder.Entity("CinemaApp.Models.Provole", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("CinemaApp.Models.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("ContentAdmins");

                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
