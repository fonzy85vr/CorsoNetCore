using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.Repository
{
    public partial class CourcesDbContext : DbContext
    {
        public CourcesDbContext()
        {

        }

        public CourcesDbContext(DbContextOptions<CourcesDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Course> Courses { get; set; }
        //public virtual DbSet<Lesson> Lessons {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/courses.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.OwnsOne(course => course.CurrentPrice, builder =>
                {
                    builder.Property(money => money.Currency).HasConversion<string>();
                });
                entity.OwnsOne(course => course.FullPrice, builder =>
                {
                    builder.Property(money => money.Currency).HasConversion<string>();
                });
            });
        }
    }
}