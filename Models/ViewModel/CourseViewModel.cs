using CorsoNetCore.Models.DataTypes;

namespace CorsoNetCore.Models.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public double Rating { get; set; }
        public Money CurrentPrice { get; set; }
        public Money Price { get; set; }
    }
}