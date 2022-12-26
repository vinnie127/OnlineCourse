using ITHSCourseSchool.Models;
using System.Linq.Expressions;

namespace ITHSCourseSchool.Repository.IRepository
{
    public interface ICourseRepository
    {

        Task<List<Course>> GetAllAsync(Expression<Func<Course, bool>> filter = null);
        Task<Course> GetAsync(Expression<Func<Course,bool>> filter = null);
        Task CreateAsync(Course entity);
        Task RemoveAsync(Course entity);
        Task SaveAsync();


        ICollection<Course> GetCourses();
        Course GetCourseById(int courseId);
        Course GetCourseByTitle(string courseTitle);
        bool CreateCourse(Course courseId);
       
        bool DeleteCourse(Course courseId);
        bool UpdateCourse(Course course);
        public ICollection<Course> GetUsers(int courseId);

        bool Save();



    }
}
