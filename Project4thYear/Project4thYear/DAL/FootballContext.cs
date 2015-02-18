using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project4thYear.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Project4thYear.DAL
{
    public class FootballContext : DbContext
    {
        public FootballContext()
            : base("FootballContext")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<League>()
            //    .HasRequired(f => f.Team)
            //    .WithRequiredDependent()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Player>()
            //    .HasRequired(f => f.Team)
            //    .WithRequiredDependent()
            //    .WillCascadeOnDelete(false);

        }
    }
}