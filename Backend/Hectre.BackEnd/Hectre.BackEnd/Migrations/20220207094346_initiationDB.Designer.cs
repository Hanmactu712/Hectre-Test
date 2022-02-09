﻿// <auto-generated />
using System;
using Hectre.BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hectre.BackEnd.Migrations
{
    [DbContext(typeof(HectreDbContext))]
    [Migration("20220207094346_initiationDB")]
    partial class initiationDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Hectre.BackEnd.Models.Chemical", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("ActiveIngredient")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ChemicalType")
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PreHarvestIntervalInDays")
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Chemical", "dlms_db");
                });
#pragma warning restore 612, 618
        }
    }
}
