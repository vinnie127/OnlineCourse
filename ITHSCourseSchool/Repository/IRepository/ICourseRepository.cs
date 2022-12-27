using ITHSCourseSchool.Models;
using System.Linq.Expressions;

namespace ITHSCourseSchool.Repository.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {

        //public ICollection<Course> GetUsers(int courseId);
        Task<Course> UpdateAsync(Course entity);


        //ICollection<Course> GetCourses();
        //Course GetCourseById(int courseId);
        //Course GetCourseByTitle(string courseTitle);
        //bool CreateCourse(Course courseId);
       
        //bool DeleteCourse(Course courseId);
        //bool UpdateCourse(Course course);
    

        //bool Save();



    }
}
