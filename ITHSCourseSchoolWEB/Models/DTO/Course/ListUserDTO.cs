using ITHSCourseSchoolWEB.Models.DTO.User;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ITHSCourseSchoolWEB.Models.DTO.Course
{
    public class ListUserDTO
    {

        public int Id { get; set; } 
        public string CourseTitle { get; set; }
        public IEnumerable<UserInListDTO> Users { get; set; }



    }
}
