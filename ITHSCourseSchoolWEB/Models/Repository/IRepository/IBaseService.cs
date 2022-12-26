namespace ITHSCourseSchoolWEB.Models.Repository.IRepository
{
    public interface IBaseService
    {

        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);


    }
}
