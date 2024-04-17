namespace CorsoNetCore.Models.ViewModel
{
    public class PaginationModel
    {
        public int Page { get; set; }
        public int ElementsPerPage { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
    }
}