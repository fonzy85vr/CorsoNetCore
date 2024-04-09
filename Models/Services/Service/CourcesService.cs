using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Service
{
    public class CoursesService : ICoursesService
    {
        public List<CourseViewModel> GetCourses()
        {
            var repo = new CoursesRepositoryMockup();
            
            return repo.GetCourses().ToList();
        }
    }
}