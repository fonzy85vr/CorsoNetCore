using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.DataTypes;
using CorsoNetCore.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.Repository
{
    public partial class CourcesDbContext : IdentityDbContext<ApplicationUser>
    {
        public CourcesDbContext(DbContextOptions<CourcesDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Course> Courses { get; set; }
        //public virtual DbSet<Lesson> Lessons {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Owned<Money>();

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
            });
        }
    }
}