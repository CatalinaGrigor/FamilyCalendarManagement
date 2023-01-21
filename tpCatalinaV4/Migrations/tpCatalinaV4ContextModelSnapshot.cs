﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tpCatalinaV4.Data;

#nullable disable

namespace tpCatalinaV4.Migrations
{
    [DbContext(typeof(tpCatalinaV4Context))]
    partial class tpCatalinaV4ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("tpCatalinaV4.Models.Horaire", b =>
                {
                    b.Property<int>("HoraireID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HoraireID"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("NbRepet")
                        .HasColumnType("int");

                    b.Property<int>("TacheID")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurID")
                        .HasColumnType("int");

                    b.HasKey("HoraireID");

                    b.HasIndex("TacheID");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Horaire");
                });

            modelBuilder.Entity("tpCatalinaV4.Models.Tache", b =>
                {
                    b.Property<int>("TacheID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TacheID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Duree")
                        .HasColumnType("float");

                    b.Property<bool>("Repetitive")
                        .HasColumnType("bit");

                    b.HasKey("TacheID");

                    b.ToTable("Tache");
                });

            modelBuilder.Entity("tpCatalinaV4.Models.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UtilisateurID"), 1L, 1);

                    b.Property<string>("NomComplet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UtilisateurID");

                    b.ToTable("Utilisateur");
                });

            modelBuilder.Entity("tpCatalinaV4.Models.Horaire", b =>
                {
                    b.HasOne("tpCatalinaV4.Models.Tache", "Tache")
                        .WithMany("Horaire")
                        .HasForeignKey("TacheID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tpCatalinaV4.Models.Utilisateur", "Utilisateur")
                        .WithMany("Horaire")
                        .HasForeignKey("UtilisateurID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tache");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("tpCatalinaV4.Models.Tache", b =>
                {
                    b.Navigation("Horaire");
                });

            modelBuilder.Entity("tpCatalinaV4.Models.Utilisateur", b =>
                {
                    b.Navigation("Horaire");
                });
#pragma warning restore 612, 618
        }
    }
}
