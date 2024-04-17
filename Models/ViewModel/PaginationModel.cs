using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoNetCore.Models.ViewModel
{
    public class PaginationModel
    {
        public int Page { get; set; }
        public int ElementsPerPage { get; set; }
        public int TotalElements {get;set;}

        public PaginationModel() {
            Page = 1;
            ElementsPerPage = 10;
        }
    }
}