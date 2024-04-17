using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Services.Common
{
    public abstract class SearchService<T> where T : class
    {
        public async Task<PaginatedResult<T>> Search(PaginationModel model){
            var fixedModel = FixModel(model);
            var toRet = await SearchInternal(fixedModel);
            toRet = PostSearch(toRet);

            return toRet;
        }

        protected abstract Task<PaginatedResult<T>> SearchInternal(PaginationModel model);

        protected virtual PaginationModel FixModel(PaginationModel model){
            var toRet = new PaginationModel();

            toRet.TotalPages = Math.Max(1, model.TotalPages);
            toRet.Page = model.Page <= toRet.TotalPages ? Math.Max(1, model.Page) : toRet.TotalPages;
            toRet.ElementsPerPage = Math.Max(10, model.ElementsPerPage);
            toRet.TotalElements = model.TotalElements;
            toRet.Offset = toRet.ElementsPerPage * (toRet.Page - 1);

            return toRet;
        }

        protected virtual PaginatedResult<T> PostSearch(PaginatedResult<T> model) {
            model.TotalPages = (int)Math.Ceiling((decimal)model.TotalElements / model.ElementsPerPage);

            return model;
        }
    }
}