using CorsoNetCore.Models.Services.ApplicationLayer.Common;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public interface ICoursesService : ISearchService<CourseViewModel>
    {
        Task<CourseDetailViewModel?> GetDetail(int id);
        Task<bool> Subscribe(int id);
        Task<bool> IsSubscribed(int id);
        Task<string> GetPaymentUrl(int courseId);
    }
}