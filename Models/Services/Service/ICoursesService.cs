using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Service
{
    public interface ICoursesService
    {
        Task<PaginatedResult<CourseViewModel>> GetCourses(PaginationModel model);
    }
}