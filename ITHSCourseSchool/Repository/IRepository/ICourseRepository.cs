using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.Course;
using System.Linq.Expressions;

namespace ITHSCourseSchool.Repository.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {

        //public ICollection<Course> GetUsers(int courseId);
        Task<Course> UpdateAsync(Course entity);
        //public  Task<Course> GetUsers(Expression<Func<Course, bool>>? filter = null);
        public ICollection<Course> GetUsers(int courseId);

        //ICollection<Course> GetCourses();
        //Course GetCourseById(int courseId);
        //Course GetCourseByTitle(string courseTitle);
        //bool CreateCourse(Course courseId);

        //bool DeleteCourse(Course courseId);
        //bool UpdateCourse(Course course);


        //bool Save();



    }
}
