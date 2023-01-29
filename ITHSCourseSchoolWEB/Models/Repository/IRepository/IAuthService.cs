using ITHSCourseSchoolWEB.Models.DTO.User;

namespace ITHSCourseSchoolWEB.Models.Repository.IRepository
{
    public interface IAuthService
    {




        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO objToCreate);
     
        public Task<T> GetCoursesFromList<T>(string Id, string token);
        public Task<T> DeleteCourseAsync<T>(CourseToAdd obj, string token);


    }
}
