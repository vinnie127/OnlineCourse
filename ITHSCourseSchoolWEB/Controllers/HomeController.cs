using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models;
using ITHSCourseSchoolWEB.Models.DTO.Course;
using ITHSCourseSchoolWEB.Models.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ITHSCourseSchoolWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseRepository _cRepo;

        public HomeController(ILogger<HomeController> logger, ICourseRepository cRepo)
        {
            _logger = logger;
            _cRepo = cRepo;
        }

        public async Task<IActionResult> Index()
        {

            List<ViewCourseDetailsDTO> list = new();

            var response = await _cRepo.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ViewCourseDetailsDTO>>(Convert.ToString(response.Result));
            }
            return View(list);


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}