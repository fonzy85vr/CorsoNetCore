using CorsoNetCore.Models.Services.Repository;
using CorsoNetCore.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CorsoNetCore.Models.Services.ApplicationLayer
{
    public class LessonsSearchService : ILessonService
    {
        private readonly CourcesDbContext _dbContext;

        public LessonsSearchService(CourcesDbContext dbContext){
            _dbContext = dbContext;
        }

        public async Task<List<LessonViewModel>> Search(int courseId)
        {
            var toRet = _dbContext.Lessons.AsNoTracking().Where(lesson => lesson.COurseId == courseId).Select(
                lesson => 
                    new LessonViewModel {
                        Description = lesson.Description,
                        Title = lesson.Title,
                        Id = lesson.Id
                    }
            );

            return await toRet.ToListAsync();
        }
    }
}