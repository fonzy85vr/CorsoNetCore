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
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<CourseSubscriptions> CourseSubscriptions { get; set; }

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

                entity.HasMany(course => course.Lessons)
                    .WithOne(lesson => lesson.Course)
                    .HasForeignKey(lesson => lesson.COurseId);

                entity.HasMany(course => course.UserSubscripted)
                    .WithMany(user => user.CourseSubscriptions)
                    .UsingEntity<CourseSubscriptions>(
                        user => user.HasOne<ApplicationUser>().WithMany().HasForeignKey(u => u.UserId),
                        course => course.HasOne<Course>().WithMany().HasForeignKey(c => c.CourseId)
                    );
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lessons");
            });
        }
    }
}