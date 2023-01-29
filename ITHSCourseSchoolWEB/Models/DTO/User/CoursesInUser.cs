using ITHSCourseSchoolWEB.Models.DTO.Course;

namespace ITHSCourseSchoolWEB.Models.DTO.User
{
    public class CoursesInUser
    {


        public ICollection<CourseInfoClient>? Courses { get; set; } = new List<CourseInfoClient>();





    }
}
