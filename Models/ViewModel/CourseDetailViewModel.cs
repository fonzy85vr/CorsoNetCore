namespace CorsoNetCore.Models.ViewModel
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public string Description { get; set; }
        public List<LessonViewModel> Lessons { get; set; }

        public TimeSpan TotalLessonDuration {get {
            return TimeSpan.FromSeconds(Lessons.Sum(lesson => lesson.Duration.TotalSeconds));
        }}
    }
}