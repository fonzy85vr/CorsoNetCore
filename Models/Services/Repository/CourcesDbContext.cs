using CorsoNetCore.Models.DataTypes;
using CorsoNetCore.Models.DataTypes.Enums;
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

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HaveConversion<double>();
            configurationBuilder.Properties<Currency>().HaveConversion<string>();
        }

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