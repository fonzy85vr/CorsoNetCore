namespace CorsoNetCore.Models.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public int COurseId {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public TimeSpan Duration {get;set;}

        public Course Course {get;set;}
    }
}