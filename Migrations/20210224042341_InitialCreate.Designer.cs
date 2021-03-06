﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PadoruHelperBotDAL;

namespace PadoruHelperBotDAL.Migrations.Migrations
{
    [DbContext(typeof(PadoruHelperContext))]
    [Migration("20210224042341_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("PadoruHelperBotDAL.Models.UserSubscriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Adventure")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<bool>("Works")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("UserSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
