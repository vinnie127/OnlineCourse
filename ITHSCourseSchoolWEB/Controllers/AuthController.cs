using ITHSCourse_Utility;
using ITHSCourseSchoolWEB.Models;
using ITHSCourseSchoolWEB.Models.DTO.User;
using ITHSCourseSchoolWEB.Models.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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






        //[HttpGet]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LoginUser()
        //{
        //    var accessToken = await HttpContext.GetTokenAsync(SD.SessionToken);
        //    return RedirectToAction(nameof(Index), "Home");

        //    //LoginRequestDTO obj = new();
        //    //return View(obj);



        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LoginUser(LoginRequestDTO obj)
        //{

        //    APIResponse response = await _authService.LoginAsync<APIResponse>(obj);

        //    if (response !=null && response.IsSuccess)
        //    {
        //        LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
               
        //        var handler = new JwtSecurityTokenHandler();
        //        var jwt = handler.ReadJwtToken(model.Token);

        //        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //        identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "name").Value));
        //        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
        //        var principal = new ClaimsPrincipal(identity);

        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        //        TempData["user"] = obj.UserName; TempData.Keep("User");


        //        HttpContext.Session.SetString(SD.SessionToken, model.Token);
        //        return RedirectToAction("Index", "Home");
        //    }

        //    else
        //    {
        //        ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
        //        return View(obj);
        //    }


        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {

            APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);
            if (result !=null && result.IsSuccess)
            {

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


     
        public async Task<IActionResult> AddCourse(int id)
        {

                CourseToAdd courseToAdd = new CourseToAdd();
            
                courseToAdd.CourseId = id;


            string userName = User.FindFirstValue(ClaimTypes.Name);

            courseToAdd.userName = userName;

            var response = await _authService.AddCourseAsync<APIResponse>(courseToAdd/*, await HttpContext.GetTokenAsync(SD.SessionToken)*/);
            if (response != null && response.IsSuccess)
            {
            CourseToAdd model = JsonConvert.DeserializeObject<CourseToAdd>(Convert.ToString(response.Result));
                //return RedirectToAction("Courses", "Index");
                return RedirectToAction("LoggaIn");
            }
            return NotFound();

        }
       







    }
}
