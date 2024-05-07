namespace CorsoNetCore.Models.ViewModel
{
    public class PaginatedResult<T> : PaginationModel
    {
        public List<T> Items { get; set; }
    }
}