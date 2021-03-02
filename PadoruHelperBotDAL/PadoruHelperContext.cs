using Microsoft.EntityFrameworkCore;
using PadoruHelperBotDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PadoruHelperBotDAL
{
    public class PadoruHelperContext: DbContext
    {
        public PadoruHelperContext(DbContextOptions<PadoruHelperContext> options) : base(options) { }

        public DbSet<AlertPetition> AlertPetitions { get; set; }
        public DbSet<UserSubscriptions> UserSubscriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(t => t.Team)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.TeamId)
                .IsRequired(false);

            modelBuilder.Entity<UserSubscriptions>()
                .HasKey(x => new { x.UserId, x.GuildId });
            modelBuilder.Entity<UserSubscriptions>()
                .HasOne(u => u.User)
                .WithMany(s => s.Subscriptions);

            modelBuilder.Entity<AlertPetition>()
                .HasKey(a => a.Id );

            modelBuilder.Entity<Team>()
                .HasKey(t => t.Id);
        }
    }
}
