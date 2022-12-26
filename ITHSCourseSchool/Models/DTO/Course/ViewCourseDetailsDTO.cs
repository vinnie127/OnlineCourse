using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.User;
using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchool.Models.DTO.Course
{
    public class ViewCourseDetailsDTO
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public ICollection<UserViewOnly>? Users { get; set; } = new List<UserViewOnly>();
    }
}
