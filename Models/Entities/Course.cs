using CorsoNetCore.Models.DataTypes;

namespace CorsoNetCore.Models.Entities
{
    public partial class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
        public Money CurrentPrice { get; set; }
        public Money FullPrice { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<ApplicationUser> UserSubscripted { get; set; }
    }
}