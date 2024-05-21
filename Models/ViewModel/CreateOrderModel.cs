using CorsoNetCore.Models.DataTypes;

namespace CorsoNetCore.Models.ViewModel
{
    public class CreateOrderModel
    {
        public Money Amount { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }
}