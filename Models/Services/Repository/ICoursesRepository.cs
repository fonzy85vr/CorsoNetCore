using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Repository
{
    public interface ICoursesRepository
    {
        IEnumerable<CourseViewModel> GetCourses();
    }
}