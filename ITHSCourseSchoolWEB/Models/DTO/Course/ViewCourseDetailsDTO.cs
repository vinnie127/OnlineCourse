using ITHSCourseSchoolWEB.Models.DTO.User;
using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchoolWEB.Models.DTO.Course
{
    public class ViewCourseDetailsDTO
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public ICollection<UserDTO>? Users { get; set; } = new List<UserDTO>();
    }
}
