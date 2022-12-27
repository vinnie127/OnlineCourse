using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models.DTO.Course;
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
                Url = courseUrl +"/api/Course",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = courseUrl +"/api/Course" + id,
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
                Url = courseUrl +"/api/Course/" + Id,
                Token = token
            });


        }

        public Task<T> UpdateAsync<T>(CreateCourseDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = courseUrl +"/api/Course" + dto.Id,
                Token = token
            });
        }


        public async Task<IEnumerable<ListUserDTO>> GetStudents(string url, int id)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, url + id);
            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<ListUserDTO>>(jsonString);

            }

            return null;






        }

    }

}

