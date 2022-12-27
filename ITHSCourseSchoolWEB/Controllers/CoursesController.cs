using AutoMapper;
using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models;
using ITHSCourseSchoolWEB.Models.DTO.Course;
using ITHSCourseSchoolWEB.Models.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ITHSCourseSchoolWEB.Controllers
{
    public class CoursesController : Controller
    {

        private readonly ICourseRepository _cRepo;
        private readonly IMapper _mapper;


        public CoursesController(ICourseRepository cRepo, IMapper mapper)
        {
            _cRepo = cRepo;
            _mapper = mapper;
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

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCourse()
        {

            

            return View();



        }


        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO model)
        {


            if (ModelState.IsValid)
            {

                var response = await _cRepo.CreateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);


        }


        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCourse(int id)
        {

          ViewBag.CourseId = id;

            var response = await _cRepo.GetAsync<APIResponse>(id, await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {

                Course model = JsonConvert.DeserializeObject<Course>(Convert.ToString(response.Result));
                return View(_mapper.Map<CreateCourseDTO>(model));
            }
            return NotFound();

        }

        //[Authorize(Roles = "admin")]
        [HttpPatch]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(CreateCourseDTO course)
        {


            if (ModelState.IsValid)
            {
                TempData["success"] = "Villa updated successfully";
                var response = await _cRepo.UpdateAsync<APIResponse>(course, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(course);




        }
               
        public async Task<IActionResult> Delete(int id)
        {

            var response = await _cRepo.GetAsync<APIResponse>(id, await HttpContext.GetTokenAsync("access_token"));
            if (response != null && response.IsSuccess)
            {
                CreateCourseDTO model = JsonConvert.DeserializeObject<CreateCourseDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();

        }

        //public async Task<IActionResult> ListOfUsersInCourse(int id)
        //{


        //    var result = await _cRepo.GetStudents(SD.StudentsInCourseAPI, id);

        //    var resultJson = Json(new { data = result.FirstOrDefault()?.Users });

        //    return resultJson;


        //}

        public async Task<IActionResult> StudentList(int id)
        {
            var StudentList = new ListUserDTO();
            ViewBag.Id = id;
            return View(StudentList);
        }








    }
}
