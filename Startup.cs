using CorsoNetCore.Models.Entities;
using ApplicationLayer = CorsoNetCore.Models.Services.ApplicationLayer;
using CorsoNetCore.Models.Services.Repository;
using Microsoft.EntityFrameworkCore;
using CorsoNetCore.Models.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CorsoNetCore.Models.Authorization;

namespace CorsoNetCore
{
    public static class Startup
    {
        public static void SetupServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMvc();

            builder.Services.AddDbContextPool<CourcesDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<CourcesDbContext>();

            builder.Services.AddResponseCaching();

            // registro i servizi applicativi
            ApplicationLayer.Startup.SetupServices(builder);

            builder.Services.AddSingleton<IEmailSender, MailService>();
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

        public static void SetupOptions(WebApplicationBuilder builder)
        {
            builder.Services.Configure<SmtpOption>(builder.Configuration.GetSection("Smtp"));
        }
    }
}