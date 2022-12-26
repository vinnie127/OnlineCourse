using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchoolWEB.Models
{
    public class Course
    {


        [Key]
        public int Id { get; set; }
        [Required]
        public string CourseTitle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CourseStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CourseEnd { get; set; }
        public List<LocalUser>? Users { get; set; } = new List<LocalUser>();


    }
}
