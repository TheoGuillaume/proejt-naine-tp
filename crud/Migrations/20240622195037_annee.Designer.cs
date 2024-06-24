﻿// <auto-generated />
using System;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace crud.Migrations
{
    [DbContext(typeof(RazorPageDbContext))]
    [Migration("20240622195037_annee")]
    partial class annee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Crud.Models.Domain.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PosteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Salary")
                        .HasColumnType("bigint");

                    b.Property<string>("codeEmployee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("heureDeTravail")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PosteId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Crud.Models.Domain.Paye", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("HeureNuit")
                        .HasColumnType("int");

                    b.Property<string>("annee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateOfPayment")
                        .HasColumnType("datetime2");

                    b.Property<int>("heureDimanche")
                        .HasColumnType("int");

                    b.Property<int>("heureTotal")
                        .HasColumnType("int");

                    b.Property<string>("mois")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nbMois")
                        .HasColumnType("int");

                    b.Property<int>("nbSemaine")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Paye");
                });

            modelBuilder.Entity("Crud.Models.Domain.Poste", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NamePoste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Poste");
                });

            modelBuilder.Entity("Crud.Models.Domain.Employee", b =>
                {
                    b.HasOne("Crud.Models.Domain.Poste", "Poste")
                        .WithMany("Employees")
                        .HasForeignKey("PosteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Poste");
                });

            modelBuilder.Entity("Crud.Models.Domain.Paye", b =>
                {
                    b.HasOne("Crud.Models.Domain.Employee", "Employee")
                        .WithMany("Payes")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Crud.Models.Domain.Employee", b =>
                {
                    b.Navigation("Payes");
                });

            modelBuilder.Entity("Crud.Models.Domain.Poste", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}