using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Services.Common
{
    public interface ISearchService<T> where T : class
    {
        Task<PaginatedResult<T>> Search(PaginationModel model);
    }
}