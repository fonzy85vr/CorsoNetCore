using CorsoNetCore.Models.Services.Services.Common;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Service
{
    public interface ICoursesService : ISearchService<CourseViewModel>
    {
    }
}