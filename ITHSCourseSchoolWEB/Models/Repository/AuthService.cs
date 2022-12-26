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
                ApiType = SDApi.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/login"
            });
        }

      

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SDApi.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/Users/register"
            });
        }



    }
}
