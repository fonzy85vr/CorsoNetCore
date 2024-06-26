namespace CorsoNetCore.Models.ViewModel
{
    public class PaginationModel
    {
        public int Page { get; set; }
        public int ElementsPerPage { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int Offset { get; set; }
        public string OrderBy { get; set; }
        public bool? IsAscending { get; set; }
    }
}