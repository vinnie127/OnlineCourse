using ITHSCourseSchool.Data;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.Course;
using ITHSCourseSchool.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITHSCourseSchool.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {


        private readonly ApplicationDbContext _db;
        internal DbSet<Course> dbSet;

        //test
        public CourseRepository(ApplicationDbContext db) :base(db)
        {

            _db = db;
            this.dbSet = _db.Set<Course>();


        }
       
            //public async Task<Course> GetUsers(Expression<Func<Course, bool>>? filter = null)
            //{
            //    IQueryable<Course> query = dbSet;
            //    if (filter != null)
            //    {
            //        query = query.Where(filter);
            //    }

            //    return await query.FirstOrDefaultAsync();
            //}


        public   ICollection<Course> GetUsers(int courseId)
        {
           
            return  _db.Course.Where(c => c.Id == courseId).Include(c => c.Users).ToList();

        }




        public async Task<Course> UpdateAsync(Course entity)
        {
            _db.Course.Update(entity);
           await _db.SaveChangesAsync();
            return entity;
        }



        //public bool UpdateCourse(Course course)
        //{
        //    _db.Course.Update(course);
        //    return Save();

        //}
        //public bool CreateCourse(Course courseId)
        //{

        //    var SearchForCourse = GetCourseByTitle(courseId.CourseTitle);
        //    if (SearchForCourse == null)
        //    {

        //        _db.Course.Add(courseId);

        //        return Save();

        //    }

        //    else
        //    {
        //        return false;
        //    }



        //}
        //public bool DeleteCourse(Course courseId)
        //{

        //    _db.Course.Remove(courseId);

        //    return Save();


        //}
        //public Course GetCourseById(int courseId)
        //{
        //    return _db.Course.FirstOrDefault(a => a.Id == courseId);

        //}
        //public Course GetCourseByTitle(string courseTitle)
        //{



        // return _db.Course.FirstOrDefault(a=>a.CourseTitle == courseTitle);





        //}


        //public ICollection<Course> GetCourses()
        //{

        //    return _db.Course.OrderBy(a=>a.CourseTitle).ToList();


        //}
        //public bool Save()
        //{
        //    return _db.SaveChanges() >= 0 ? true : false;
        //}





    }
}
