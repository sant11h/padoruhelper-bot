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

        public DbSet<AlertPetition> AlertPetition { get; set; }
        public DbSet<UserSubscriptions> UserSubscriptions { get; set; }
    }
}
