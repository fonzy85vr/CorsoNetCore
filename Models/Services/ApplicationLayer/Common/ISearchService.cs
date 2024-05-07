using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.ApplicationLayer.Common
{
    public interface ISearchService<T> where T : class
    {
        Task<PaginatedResult<T>?> Search(PaginationModel model);
    }
}