using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public interface ILessonService
    {
        Task<List<LessonViewModel>> Search(int courseId);
    }
}