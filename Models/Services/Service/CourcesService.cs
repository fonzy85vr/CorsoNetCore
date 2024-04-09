using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.Service
{
    public class CoursesService : ICoursesService
    {
        private readonly CourcesDbContext _dbContext;
        private readonly ILogger<CoursesService> _logger;

        public CoursesService( ILogger<CoursesService> logger, CourcesDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<CourseViewModel>> GetCourses()
        {
            _logger.LogInformation("Recuperiamo la lista dei corsi");
            var queryCourses = _dbContext.Courses.AsNoTracking().Select(course => 
            new CourseViewModel{
                    Author = course.Author,
                    CurrentPrice = course.CurrentPrice,
                    Id = course.Id,
                    ImagePath = course.ImagePath,
                    Price = course.FullPrice,
                    Rating = course.Rating,
                    Title = course.Title
                }
            );
            var courses = await queryCourses.ToListAsync();
            
            return courses;
        }
    }
}