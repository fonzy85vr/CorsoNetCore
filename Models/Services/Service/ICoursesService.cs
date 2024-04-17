using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Service
{
    public interface ICoursesService
    {
        Task<List<CourseViewModel>> GetCourses(BaseSearchInputModel model);
    }
}