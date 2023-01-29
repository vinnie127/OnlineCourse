using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models.DTO.User;
using ITHSCourseSchoolWEB.Models.Repository.IRepository;

namespace ITHSCourseSchoolWEB.Models.Repository
{
    public class AuthService : BaseService, IAuthService
    {

        private readonly IHttpClientFactory _clientFactory;

        private string villaUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:SchoolAPI");

        }


        public  Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/login"
            });
        }

      

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/register"
            });
        }


    

        public Task<T> GetCoursesFromList<T>(string Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/Users/GetCourseFromID/" + Id,
                Token = token
            });


        }


        public Task<T> DeleteCourseAsync<T>(CourseToAdd obj, string token)
        {



            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/removeCourse",
                Token = token
            });
        }





    }
}
