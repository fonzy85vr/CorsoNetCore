using CorsoNetCore.Models.Services.ApplicationLayer;
using CorsoNetCore.Models.ViewModel;
using Microsoft.Extensions.Caching.Memory;

namespace CorsoNetCore.Models.Services
{
    public class MemoryCachedCourseService : ICachedCourseService
    {
        private readonly IMemoryCache _cache;
        private readonly ICoursesService _service;
        public MemoryCachedCourseService(IMemoryCache cache, ICoursesService service)
        {
            _cache = cache;
            _service = service;
        }

        public Task<CourseDetailViewModel?> GetDetail(int id)
        {
            return _cache.GetOrCreateAsync($"Courses_{id}", cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                    return _service.GetDetail(id);
                });
        }

        public Task<string> GetPaymentUrl(int courseId)
        {
            return _service.GetPaymentUrl(courseId);
        }

        public Task<bool> IsSubscribed(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<CourseViewModel>?> Search(PaginationModel model)
        {
            if (model.Page < 3)
            {
                return _cache.GetOrCreateAsync($"Courses_Page{model.Page}_orderBy{model.OrderBy}-{model.IsAscending}", cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                    return _service.Search(model);
                });
            }

            return _service.Search(model);
        }

        public Task<bool> Subscribe(int id)
        {
            return _service.Subscribe(id);
        }
    }
}