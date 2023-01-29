using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models;
using ITHSCourseSchoolWEB.Models.DTO.Course;
using ITHSCourseSchoolWEB.Models.DTO.User;
using ITHSCourseSchoolWEB.Models.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ITHSCourseSchoolWEB.Controllers
{
    public class AuthController : Controller
    {


        private readonly IAuthService _authService;


        public AuthController(IAuthService authservice)
        {

            _authService = authservice;

        }



        [HttpGet]
        public async Task<IActionResult> LoggaIn()
        {



            LoginRequestDTO obj = new();
            return View(obj);



        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoggaIn(LoginRequestDTO obj)
        {

            APIResponse response = await _authService.LoginAsync<APIResponse>(obj);

            if (response != null && response.IsSuccess)
            {
                LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(model.Token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "name").Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
               


                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
                return View(obj);
            }


        }



        

        [HttpGet]
        public IActionResult Register()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "admin", Value = "0" });

            items.Add(new SelectListItem { Text = "teacher", Value = "1" });

            items.Add(new SelectListItem { Text = "student", Value = "2", Selected = true });

            ViewBag.MovieType = items;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {

            obj.Role = Request.Form["Roles"].ToString();

            APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);
            if (result !=null && result.IsSuccess)
            {

                TempData["AlertMessage"] = "You are now registered!";

                return RedirectToAction("LoggaIn");

            }

           
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            SignOut("Cookies", "oidc");

            HttpContext.Session.SetString(SD.SessionToken, "");
           

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {

            return View();
        }



        [HttpGet]
         public async Task<IActionResult> YourCourseList()
        {
            
            
            //List<CourseInfoDTO> list = new();
            

            string userId = User.FindFirstValue(ClaimTypes.Name);


            var response = await _authService.GetCoursesFromList<APIResponse>(userId, await HttpContext.GetTokenAsync(SD.SessionToken));
            
              var list  = JsonConvert.DeserializeObject<IEnumerable<CoursesInUser>>(Convert.ToString(response.Result));
            

            
            var resultJson = Json(new { data = list.FirstOrDefault()?.Courses });



            return resultJson;


        }


        public async Task<IActionResult> YourCourseListView()
        {

            var courseList = new CoursesInUser();
           

            
          
            return View(courseList);
        }



   
        public async Task<IActionResult> DeleteCourseFromList(int id)
        {

            CourseToAdd courseToRemove = new CourseToAdd();

            courseToRemove.CourseId = id;



            string UserName = User.FindFirstValue(ClaimTypes.Name);
            courseToRemove.UserId = UserName;

            var response = await _authService.DeleteCourseAsync<APIResponse>(courseToRemove, await HttpContext.GetTokenAsync(SD.SessionToken));
            if (response.IsSuccess)
            {
                CourseToAdd model = JsonConvert.DeserializeObject<CourseToAdd>(Convert.ToString(response.Result));
                //return RedirectToAction("Courses", "Index");
                
                return RedirectToAction("YourCourseListView", "Auth");
            }
            return NotFound();

        }


    }
}
