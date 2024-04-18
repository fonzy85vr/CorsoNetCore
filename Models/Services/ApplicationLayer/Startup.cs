using CorsoNetCore.Models.Services.ApplicationLayer;
using CorsoNetCore.Models.Services.ApplicationLayer;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public static class Startup
    {
        public static void SetupServices(WebApplicationBuilder builder){
            builder.Services.AddTransient<ICoursesService, CourseSearchService>();
        }
    }
}