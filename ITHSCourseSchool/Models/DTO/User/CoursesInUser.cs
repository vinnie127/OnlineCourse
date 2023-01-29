using ITHSCourseSchool.Models.DTO.Course;
using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchool.Models.DTO.User
{
    public class CoursesInUser
    {

        public ICollection<CourseInfoClient>? Courses { get; set; } = new List<CourseInfoClient>();

    }
}
