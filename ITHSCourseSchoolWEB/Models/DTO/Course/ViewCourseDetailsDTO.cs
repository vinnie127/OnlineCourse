using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchoolWEB.Models.DTO.Course
{
    public class ViewCourseDetailsDTO
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public ICollection<LocalUser>? Users { get; set; } = new List<LocalUser>();
    }
}
