namespace CorsoNetCore.Models.ViewModel
{
    public class SubscriptionCreationModel
    {
        public int CourseId { get; set; }
        public string TransactionId { get; set; }
        public DateOnly DateSubscription { get; set; }
        public string PaymentMethod { get; set; }
    }
}