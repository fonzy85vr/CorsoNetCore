using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoNetCore.Models.Entities
{
    public class CourseSubscriptions
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public DateOnly DateSubscription { get; set; }
        public double UserVote { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId {get;set;}
    }
}