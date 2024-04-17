using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoNetCore.Models.ViewModel
{
    public class PaginatedResult<T> : PaginationModel
    {
        public List<T> Items { get; set; }
    }
}