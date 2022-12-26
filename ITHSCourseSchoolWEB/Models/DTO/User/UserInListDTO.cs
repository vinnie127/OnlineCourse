using System.ComponentModel.DataAnnotations;

namespace ITHSCourseSchoolWEB.Models.DTO.User
{
    public class UserInListDTO
    {
        [Key]
        public string Id { get; set; }
        public string userName { get; set; }
        public string name { get; set; }



    }
}
