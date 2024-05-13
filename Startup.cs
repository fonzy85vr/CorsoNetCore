using Services = CorsoNetCore.Models;
using CorsoNetCore.Models.Options;

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
            builder.Services.Configure<PaymentOption>(builder.Configuration.GetSection("Paypal"));
        }
    }
}