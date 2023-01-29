using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models.DTO.Course;
using ITHSCourseSchoolWEB.Models.DTO.User;
using ITHSCourseSchoolWEB.Models.Repository.IRepository;
using Newtonsoft.Json;

namespace ITHSCourseSchoolWEB.Models.Repository
{
    public class CourseRepository : BaseService, ICourseRepository
    {




        private readonly IHttpClientFactory _clientFactory;
        private string courseUrl;

        public CourseRepository(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {

            _clientFactory = clientFactory;
            courseUrl = configuration.GetValue<string>("ServiceUrls:SchoolAPI");

        }

        public Task<T> CreateAsync<T>(CreateCourseDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = courseUrl + "/api/Course/CreateCourse",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = courseUrl +"/api/Course/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = courseUrl +"/api/Course/Courses",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = courseUrl +"/api/Course/GetCourse/" + Id,
                Token = token
            });


        }

        public Task<T> UpdateAsync<T>(EditCourseDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PATCH,
                Data = dto,
                Url = courseUrl +"/api/Course/" + dto.Id,
                Token = token
            });
        }


        public Task<T>  GetStudents<T>(int id, string token)
        {

            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = courseUrl + "/api/Course/GetUsers/" + id,
                Token = token
            });


        }


        public Task<T> AddCourseAsync<T>(CourseToAdd obj, string token)
        {



            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = courseUrl + "/api/Users/addCourse",
                Token = token
            });
        }


    }

}

