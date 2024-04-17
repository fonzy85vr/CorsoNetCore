using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoNetCore.Models.ViewModel
{
    public class BaseSearchInputModel
    {
        public int Page { get; set; }
        public int ElementsPerPage { get; set; }

        public BaseSearchInputModel() {
            Page = 1;
            ElementsPerPage = 10;
        }
    }
}