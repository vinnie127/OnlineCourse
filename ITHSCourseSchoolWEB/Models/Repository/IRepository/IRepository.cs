using ITHSCourseSchoolWEB.Models.DTO.Course;

namespace ITHSCourseSchoolWEB.Models.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url, T objToCreate);
        Task<bool> UpdateAsync(string url, T objToUpdate);
        Task<bool> DeleteAsync(string url, int Id);

        Task<IEnumerable<ListUserDTO>> GetStudents(string url, int id);



    }
}
