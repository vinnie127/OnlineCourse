using ITHSCourseSchool.Models.DTO.Course;

namespace ITHSCourseSchool.Models.DTO.User
{
    public class UserDTO
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public ICollection<ViewCourse>? Courses { get; set; } = new List<ViewCourse>();

    }
}
