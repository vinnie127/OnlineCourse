using ITHSCourseSchoolWEB.Models.DTO.Course;
using ITHSCourseSchoolWEB.Models.DTO.User;

namespace ITHSCourseSchoolWEB.Models.Repository.IRepository
{
    public interface ICourseRepository 
    {

        Task<T> GetAsync<T>(int Id, string token);
        Task<T> GetAllAsync<T>(string token);
        Task<T> CreateAsync<T>(CreateCourseDTO dto, string token);
        Task<T> UpdateAsync<T>(EditCourseDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        public Task<T> GetStudents<T>(int id, string token);
        public Task<T> AddCourseAsync<T>(CourseToAdd obj, string token);
    }
}
