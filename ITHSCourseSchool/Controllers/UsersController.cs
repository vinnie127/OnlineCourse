using AutoMapper;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.User;
using ITHSCourseSchool.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ITHSCourseSchool.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private string secretKey;

        public UsersController(IUserRepository userRepo, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            this._response = new();
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {

            var LoginResponse = await _userRepo.Login(model);

            if (LoginResponse.User == null || string.IsNullOrEmpty(LoginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is not correct");


                return BadRequest(_response);

            }

            _response.StatusCode=HttpStatusCode.OK;
            _response.IsSuccess=true;
            _response.Result = LoginResponse;   


            return Ok(_response);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);




            }

            var user = await _userRepo.Register(model);

            if (user==null)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering, roles: student, admin or teacher");
                return BadRequest(_response);


            }

            return Ok(_response);
        }


        [HttpGet("courses")]
        [ProducesResponseType(200)]
        public IActionResult GetCourse(string userName)
        {
            var objList = _userRepo.GetCourses(userName);



            if (objList == null)
            {

                return NotFound();

            }


            var objToShow = new List<UserDTO>();

            foreach (var obj in objList)
            {

                objToShow.Add(_mapper.Map<UserDTO>(obj));

            }

            return Ok(objToShow);



        }

        [HttpPost("addCourse")]
        public IActionResult AddCourse(string userName, int CourseId)
        {

            if (userName == null || CourseId == null)
            {
                return BadRequest();
            }


            var userObj = _userRepo.AddCourse(userName, CourseId);

            return Ok();


        }



        [HttpGet("UsersInSchool")]
        [ProducesResponseType(200)]
        public IActionResult GetUsers()
        {

            var users = _userRepo.GetUsers();

            return Ok(users);



        }

        //[HttpGet("StudentsInSchool")]
        //[ProducesResponseType(200)]
        //public IActionResult StudentsInSchool()
        //{

        //    var users = _userRepo.GetStudents();



        //    return Ok(users);



        //}


        //[HttpGet("Teachers")]
        //[ProducesResponseType(200)]
        //public IActionResult TeachersInSchool()
        //{

        //    var users = _userRepo.GetTeachers();



        //    return Ok(users);



        //}



    }
}
