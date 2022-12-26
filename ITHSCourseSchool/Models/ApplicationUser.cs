using Microsoft.AspNetCore.Identity;

namespace ITHSCourseSchool.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }

        public ICollection<Course>? Courses { get; set; } = new List<Course>();

        


    }
}
