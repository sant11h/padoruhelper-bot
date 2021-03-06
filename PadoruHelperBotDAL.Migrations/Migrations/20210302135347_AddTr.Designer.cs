﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PadoruHelperBotDAL;

namespace PadoruHelperBotDAL.Migrations.Migrations
{
    [DbContext(typeof(PadoruHelperContext))]
    [Migration("20210302135347_AddTr")]
    partial class AddTr
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("PadoruHelperBotDAL.Models.AlertPetition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AlertType")
                        .HasColumnType("int");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("SendedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("AlertPetitions");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.Team", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<ulong>("RoleId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.User", b =>
                {
                    b.Property<ulong>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong?>("TeamId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("UserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.UserSubscriptions", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<bool>("Adventure")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Training")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Works")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "GuildId");

                    b.ToTable("UserSubscriptions");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.User", b =>
                {
                    b.HasOne("PadoruHelperBotDAL.Models.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.UserSubscriptions", b =>
                {
                    b.HasOne("PadoruHelperBotDAL.Models.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.Team", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("PadoruHelperBotDAL.Models.User", b =>
                {
                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
