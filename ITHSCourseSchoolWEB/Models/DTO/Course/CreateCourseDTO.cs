using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchoolWEB.Models.DTO.Course
{
    public class CreateCourseDTO
    {



        [Required]
        public string CourseTitle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CourseStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CourseEnd { get; set; }
       
    }
}
