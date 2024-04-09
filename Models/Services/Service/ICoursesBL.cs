using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Service
{
    public interface ICoursesService
    {
        List<CourseViewModel> GetCourses();
    }
}