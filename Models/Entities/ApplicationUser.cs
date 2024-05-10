using Microsoft.AspNetCore.Identity;

namespace CorsoNetCore.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Course> CourseSubscriptions { get; set; }
    }
}