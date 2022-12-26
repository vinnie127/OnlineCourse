using AutoMapper;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.Course;
using ITHSCourseSchool.Models.DTO.User;
using ITHSCourseSchool.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITHSCourseSchool.Controllers
{

    [Route("api/Course")]
    public class CourseController : Controller
    {


        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;



        public CourseController(ICourseRepository courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }


        [HttpGet("Courses")]
        [ProducesResponseType(200, Type = typeof(List<ViewCourseDetailsDTO>))]
        public IActionResult GetCourses()
        {
            
           var courses = _courseRepo.GetCourses();

            


            return Ok(courses);
            
        }


        [HttpPost("CreateCourse")]
        [Authorize(Roles= "student")]
        [ProducesResponseType(201, Type = typeof(Course))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCourse([FromBody] RegisterCourseDTO course)
        {

            if (course == null)
            {
                return BadRequest(ModelState);
            }

           
            var courseObj = _mapper.Map<Course>(course);
            
            if (!_courseRepo.CreateCourse(courseObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {courseObj.CourseTitle} or the title already exists");
                return StatusCode(500, ModelState);
            }


            return CreatedAtRoute("GetCourse", new { CourseId = courseObj.Id }, courseObj);
        }


        [HttpGet("[action]/{id:int}")]
        [ProducesResponseType(200, Type = typeof(ViewCourseDetailsDTO))]
        public IActionResult GetUsers(int id)
        {

            var objList = _courseRepo.GetUsers(id);
           
            if (objList == null)
            {
                return NotFound();
            }

            var objToShow = new List<ViewCourseDetailsDTO>();

            foreach (var obj in objList)
            {

                objToShow.Add(_mapper.Map<ViewCourseDetailsDTO>(obj));
                
            }
     
            return Ok(objToShow);

        }

        [HttpGet("{courseId:int}", Name = "GetCourse")]
        [ProducesResponseType(200, Type = typeof(Course))]
        public IActionResult GetCourse(int courseId)
        {
            var objList = _courseRepo.GetCourseById(courseId);

            if (objList == null)
            {
                return NotFound();
            }

           


            return Ok(objList);
        }


        [HttpDelete("{courseId:int}", Name ="deleteCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCourse(int courseId)
        {

            var courseObj = _courseRepo.GetCourseById(courseId);

            if (!_courseRepo.DeleteCourse(courseObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {courseObj.CourseTitle}");
                return StatusCode(500, ModelState);


            }



            return NoContent();

        }

        
        [HttpPatch("{courseId:int}", Name = "UpdateCourse")]
        public IActionResult UpdateCourse(int courseId, [FromBody] RegisterCourseDTO coursemodel)
        {
            if (coursemodel == null || courseId != coursemodel.Id)
            {
                return BadRequest(ModelState);
            }

            var courseObj = _mapper.Map<Course>(coursemodel);

            if (!_courseRepo.UpdateCourse(courseObj))
            {
                ModelState.AddModelError("", $"Something went wrong when Updating the record {coursemodel.CourseTitle}");
                return StatusCode(500, ModelState);


            }

            return Ok();

        }



    }
}
