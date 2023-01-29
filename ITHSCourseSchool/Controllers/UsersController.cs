using AutoMapper;
using ITHSCourseSchool.Data;
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
        private readonly ApplicationDbContext _db;

        public UsersController(IUserRepository userRepo, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            this._response = new();
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _db = db;

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


            var objToShow = new List<CoursesInUser>();

            foreach (var obj in objList)
            {

                objToShow.Add(_mapper.Map<CoursesInUser>(obj));

            }

            return Ok();



        }




        [HttpGet("[action]/{id:string}")]
        [ProducesResponseType(200)]
        public IActionResult GetCourseFromID(string id)
        {
            var objList = _userRepo.GetCourses(id);



            if (objList == null)
            {

                return NotFound();

            }


            var objToShow = new List<CoursesInUser>();

            foreach (var obj in objList)
            {

                objToShow.Add(_mapper.Map<CoursesInUser>(obj));
           

            }


           
            _response.Result = objToShow;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);



        }



        [HttpPost("addCourse")]
        public  async Task<ActionResult<APIResponse>> AddCourse( [FromBody] CourseToAddDTO model)
        {

            try
            {

                CourseToAddDTO addCourse = new CourseToAddDTO();

                if (model.UserId == null || model.CourseId == null)
                {
                    ModelState.AddModelError("", $"Something went wrong when adding the record {model.CourseId}");
                    return StatusCode(500, ModelState);
                }



                addCourse.UserId = model.UserId;
                addCourse.CourseId = model.CourseId;

               await _userRepo.AddCourse(addCourse);

                if (addCourse == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Error while adding course");
                    return BadRequest(_response);
                }


                return (_response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }



            return (_response);
        }


        [HttpPost("removeCourse")]
        public async Task<ActionResult<APIResponse>> RemoveCourse([FromBody] CourseToAddDTO model)
        {

            try
            {

                CourseToAddDTO addCourse = new CourseToAddDTO();

                if (model.UserId == null || model.CourseId == null)
                {
                    ModelState.AddModelError("", $"Something went wrong when adding the record {model.CourseId}");
                    return StatusCode(500, ModelState);
                }



                addCourse.UserId = model.UserId;
                addCourse.CourseId = model.CourseId;

                await _userRepo.RemoveCourse(addCourse);



                if (addCourse == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Error while adding course");
                    return BadRequest(_response);
                }


                return (_response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }



            return (_response);
        }



        [HttpGet("UsersInSchool")]
        [ProducesResponseType(200)]
        public IActionResult GetUsers()
        {

            var users = _userRepo.GetUsers();

            return Ok(users);



        }




    }
}
