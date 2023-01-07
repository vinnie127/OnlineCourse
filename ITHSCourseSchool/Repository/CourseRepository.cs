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











    }
}
