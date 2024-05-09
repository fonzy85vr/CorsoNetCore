using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.Authorization;
using CorsoNetCore.Models.Entities;
using CorsoNetCore.Models.Services;
using CorsoNetCore.Models.Services.ApplicationLayer;
using CorsoNetCore.Models.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models
{
    public class Startup
    {
        public static void RegisterServices(WebApplicationBuilder builder)
        {
            RegisterRepositoryServices(builder);
            RegisterApplicationServices(builder);
            RegisterAuthenticationServices(builder);
        }

        private static void RegisterApplicationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICoursesService, CourseSearchService>();
            builder.Services.AddTransient<ILessonService, LessonsSearchService>();
            builder.Services.AddTransient<ICachedCourseService, MemoryCachedCourseService>();
        }

        private static void RegisterRepositoryServices(WebApplicationBuilder builder)
        {
            ConfigureDbContext(builder);

            builder.Services.AddSingleton<IEmailSender, MailService>();
        }

        private static void RegisterAuthenticationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorizationHandler, SubscriberRequirementHandler>();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy("Subscriber", builder =>
                {
                    builder.Requirements.Add(new SubscriberRequirement());
                });
            });
        }

        private static void ConfigureDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextPool<CourcesDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<CourcesDbContext>();
        }
    }
}