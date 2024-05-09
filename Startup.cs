using CorsoNetCore.Models.Entities;
using Services = CorsoNetCore.Models;
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

            builder.Services.AddResponseCaching();

            Services.Startup.RegisterServices(builder);
        }

        public static void SetupOptions(WebApplicationBuilder builder)
        {
            builder.Services.Configure<SmtpOption>(builder.Configuration.GetSection("Smtp"));
        }
    }
}