using ITHSCourseSchool.Data;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.Course;
using ITHSCourseSchool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITHSCourseSchool.Repository
{
    public class CourseRepository : ICourseRepository
    {


        private readonly ApplicationDbContext _db;


        //test
        public CourseRepository(ApplicationDbContext db)
        {

            _db = db;



        }


        public bool UpdateCourse(Course course)
        {
            _db.Course.Update(course);
            return Save();

        }
        public bool CreateCourse(Course courseId)
        {

            var SearchForCourse = GetCourseByTitle(courseId.CourseTitle);
            if (SearchForCourse == null)
            {

                _db.Course.Add(courseId);

                return Save();

            }

            else
            {
                return false;
            }



        }
        public bool DeleteCourse(Course courseId)
        {
            
            _db.Course.Remove(courseId);

            return Save();


        }
        public Course GetCourseById(int courseId)
        {
            return _db.Course.FirstOrDefault(a => a.Id == courseId);

        }
        public Course GetCourseByTitle(string courseTitle)
        {



         return _db.Course.FirstOrDefault(a=>a.CourseTitle == courseTitle);
         




        }

        public ICollection<Course> GetUsers(int courseId)
        {
           return  _db.Course.Where(c => c.Id == courseId).Include(a => a.Users).ToList();




        }
        public ICollection<Course> GetCourses()
        {

            return _db.Course.OrderBy(a=>a.CourseTitle).ToList();


        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }




        public async Task<List<Course>> GetAllAsync(Expression<Func<Course, bool>> filter = null)
        {
            IQueryable<Course> query = _db.Course;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();

        }

        public async Task<Course> GetAsync(Expression<Func<Course,bool>> filter = null)
        {
            IQueryable<Course> query = _db.Course;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public Task CreateAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
