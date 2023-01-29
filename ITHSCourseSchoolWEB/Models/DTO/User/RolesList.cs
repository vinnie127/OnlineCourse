namespace ITHSCourseSchoolWEB.Models.DTO.User
{
    public static class RolesList
    {

      public static List<string> GetAll()
        {


            return new List<string>() { "admin", "teacher", "student" };



        }


    }
}
