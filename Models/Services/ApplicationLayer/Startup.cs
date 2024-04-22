namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public static class Startup
    {
        public static void SetupServices(WebApplicationBuilder builder){
            builder.Services.AddTransient<ICoursesService, CourseSearchService>();
            builder.Services.AddTransient<ICachedCourseService, MemoryCachedCourseService>();
        }
    }
}