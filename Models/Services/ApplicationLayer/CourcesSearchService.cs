using System.Security.Claims;
using CorsoNetCore.Models.Entities;
using CorsoNetCore.Models.Services.ApplicationLayer.Common;
using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public class CourseSearchService : SearchService<CourseViewModel>, ICoursesService
    {
        private readonly CourcesDbContext _dbContext;
        private readonly ILogger<CourseSearchService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPaymentGateway _paymentGateway;

        public CourseSearchService(ILogger<CourseSearchService> logger, CourcesDbContext dbContext, IHttpContextAccessor httpContextAccessor, IPaymentGateway paymentGateway)
        {
            _dbContext = dbContext;
            _logger = logger;
            _httpContext = httpContextAccessor;
            _paymentGateway = paymentGateway;
        }

        public async Task<CourseDetailViewModel?> GetDetail(int id)
        {
            var toRet = await _dbContext.Courses.AsNoTracking().Where(course => course.Id == id).Select(course =>
                new CourseDetailViewModel
                {
                    Author = course.Author,
                    CurrentPrice = course.CurrentPrice,
                    Description = course.Description,
                    Id = course.Id,
                    ImagePath = course.ImagePath,
                    Price = course.FullPrice,
                    Title = course.Title,
                    Rating = course.Rating,
                    Lessons = course.Lessons.Select(lesson => new LessonViewModel
                    {
                        Description = lesson.Description,
                        Id = lesson.Id,
                        Title = lesson.Title,
                        Duration = lesson.Duration
                    }).ToList()
                }).FirstOrDefaultAsync();

            return toRet;
        }

        public async Task<bool> IsSubscribed(int id)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var courses = await _dbContext.CourseSubscriptions.Where(subscriptions => subscriptions.CourseId == id && subscriptions.UserId == userId).ToListAsync();

            return courses != null && courses.Count == 1;
        }

        public async Task<int> Subscribe(string idTransaction)
        {

            var customToken = await _paymentGateway.Confirm(idTransaction);

            var id = int.Parse(customToken.Split(':')[1]);

            var subscribe = new CourseSubscriptions
            {
                CourseId = id,
                DateSubscription = DateOnly.FromDateTime(DateTime.Today),
                PaymentMethod = "Paypal",
                UserId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
                UserVote = 0,
                TransactionId = idTransaction
            };

            _dbContext.Add(subscribe);
            _dbContext.SaveChanges();
            return id;
        }

        public async Task<string> GetPaymentUrl(int courseId)
        {
            var course = await GetDetail(courseId);
            if (course != null)
            {
                var model = new CreateOrderModel
                {
                    CourseId = courseId,
                    Amount = new DataTypes.Money
                    {
                        Amount = course.CurrentPrice.Amount,
                        Currency = course.CurrentPrice.Currency
                    },
                    UserId = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""
                };

                return await _paymentGateway.GetPaymentUrl(model);
            }

            return "";
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

            queryCourses = queryCourses.Skip(toRet.Offset).Take(model.ElementsPerPage);

            toRet.Items = await queryCourses.ToListAsync();

            return toRet;
        }
    }
}