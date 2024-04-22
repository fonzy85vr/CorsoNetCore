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
        public Task<PaginatedResult<CourseViewModel>> Search(PaginationModel model)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            if (model.Page < 3)
            {
                return _cache.GetOrCreateAsync($"Courses_Page{model.Page}_orderBy{model.OrderBy}-{model.IsAscending}", cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                    return _service.Search(model);
                });
            }
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            return _service.Search(model);
        }
    }
}