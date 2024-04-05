using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.BusinessLogic
{
    public interface ICoursesBL
    {
        List<CourseViewModel> GetCourses();
    }
}