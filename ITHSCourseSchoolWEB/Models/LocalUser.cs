﻿namespace ITHSCourseSchoolWEB.Models
{
    public class LocalUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }


        public ICollection<Course>? Courses { get; set; } = new List<Course>();


    }
}
