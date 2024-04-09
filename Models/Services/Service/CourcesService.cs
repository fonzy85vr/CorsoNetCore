using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.Service
{
    public class CoursesService : ICoursesService
    {
        private readonly CourcesDbContext _dbContext;

        public CoursesService(CourcesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CourseViewModel>> GetCourses()
        {
            var courses = await _dbContext.Courses.AsNoTracking().Select(course => 
            new CourseViewModel{
                    Author = course.Author,
                    CurrentPrice = course.CurrentPrice,
                    Id = course.Id,
                    ImagePath = course.ImagePath,
                    Price = course.FullPrice,
                    Rating = course.Rating,
                    Title = course.Title
                }
            ).ToListAsync();
            
            return courses;
        }
    }
}