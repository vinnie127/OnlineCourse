using ITHSCourseSchoolWEB.Models.DTO.Course;

namespace ITHSCourseSchoolWEB.Models.Repository.IRepository
{
    public interface ICourseRepository 
    {

        Task<T> GetAsync<T>(int Id, string token);
        Task<T> GetAllAsync<T>(string token);
        Task<T> CreateAsync<T>(CreateCourseDTO dto, string token);
        Task<T> UpdateAsync<T>(EditCourseDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        public Task<IEnumerable<ListUserDTO>> GetStudents(string url, int id);
    }
}
