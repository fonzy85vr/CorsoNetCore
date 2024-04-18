using CorsoNetCore.Models.Services.ApplicationLayer.Common;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public interface ICoursesService : ISearchService<CourseViewModel>
    {
    }
}