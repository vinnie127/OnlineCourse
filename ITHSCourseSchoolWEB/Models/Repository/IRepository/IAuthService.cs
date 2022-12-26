using ITHSCourseSchoolWEB.Models.DTO.User;

namespace ITHSCourseSchoolWEB.Models.Repository.IRepository
{
    public interface IAuthService
    {




        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO objToCreate);


    }
}
