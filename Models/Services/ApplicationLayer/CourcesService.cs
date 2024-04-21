using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.Services.ApplicationLayer.Common;
using CorsoNetCore.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public class CourseSearchService : SearchService<CourseViewModel>, ICoursesService
    {
        private readonly CourcesDbContext _dbContext;
        private readonly ILogger<CourseSearchService> _logger;

        public CourseSearchService(ILogger<CourseSearchService> logger, CourcesDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        protected override async Task<PaginatedResult<CourseViewModel>> SearchInternal(PaginationModel model)
        {
            _logger.LogInformation("Recuperiamo la lista dei corsi");
            var queryCourses = _dbContext.Courses.AsNoTracking().Select(course =>
            new CourseViewModel
            {
                Author = course.Author,
                CurrentPrice = course.CurrentPrice,
                Id = course.Id,
                ImagePath = course.ImagePath,
                Price = course.FullPrice,
                Rating = course.Rating,
                Title = course.Title
            }
            );

            var toRet = new PaginatedResult<CourseViewModel>()
            {
                Page = model.Page,
                ElementsPerPage = model.ElementsPerPage,
                Offset = model.Offset,
                IsAscending = model.IsAscending,
                OrderBy = model.OrderBy
            };

            toRet.TotalElements = queryCourses.Count();

            queryCourses = queryCourses.Skip(toRet.Offset).Take(model.ElementsPerPage);

            queryCourses = (model.OrderBy, model.IsAscending) switch
            {
                ("Title", true) => queryCourses.OrderBy(course => course.Title),
                ("Title", false) => queryCourses.OrderByDescending(course => course.Title),
                ("Rating", true) => queryCourses.OrderBy(course => course.Rating),
                ("Rating", false) => queryCourses.OrderByDescending(course => course.Rating),
                ("Prize", true) => queryCourses.OrderBy(course => course.CurrentPrice.Amount),
                ("Prize", false) => queryCourses.OrderByDescending(course => course.CurrentPrice.Amount),
                _ => queryCourses.OrderBy(course => course.Title)
            };

            toRet.Items = await queryCourses.ToListAsync();

            return toRet;
        }
    }
}