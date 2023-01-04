using AutoMapper;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.Course;
using ITHSCourseSchool.Models.DTO.User;
using ITHSCourseSchool.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace ITHSCourseSchool.Controllers
{

    [Route("api/Course")]
    public class CourseController : Controller
    {


        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        public CourseController(ICourseRepository courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet/*("Courses")*/]
        [Route("Courses")]
        [ProducesResponseType(200, Type = typeof(List<ViewCourseDetailsDTO>))]
        public async Task<ActionResult<APIResponse>> GetCourses()
        {

            try
            {
                IEnumerable<Course> courseList = await _courseRepo.GetAllAsync();
                _response.Result = _mapper.Map<List<ViewCourseDetailsDTO>>(courseList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;



        }



        [HttpPost("CreateCourse")]
        [ProducesResponseType(201, Type = typeof(Course))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCourse([FromBody] RegisterCourseDTO course)
        {
            try
            {




                if (await _courseRepo.GetAsync(u => u.CourseTitle.ToLower() == course.CourseTitle.ToLower()) != null)
                {

                    ModelState.AddModelError("ErrorMessages", "Course already exists!");
                    return BadRequest(ModelState);

                }


                if (course == null)
                {
                    return BadRequest(ModelState);
                }



                Course courseObj = _mapper.Map<Course>(course);

                await _courseRepo.CreateAsync(courseObj);

                _response.Result = _mapper.Map<ViewCourseDetailsDTO>(courseObj);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCourse", new { courseId = courseObj.Id }, _response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }



        //[HttpGet("[action]/{id:int}")]
        //[ProducesResponseType(200, Type = typeof(ViewCourseDetailsDTO))]
        //public IActionResult GetUsers(int id)
        //{

        //    var objList = _courseRepo.GetUsers(id);

        //    if (objList == null)
        //    {
        //        return NotFound();
        //    }

        //    var objToShow = new List<ViewCourseDetailsDTO>();

        //    foreach (var obj in objList)
        //    {

        //        objToShow.Add(_mapper.Map<ViewCourseDetailsDTO>(obj));

        //    }

        //    return Ok(objToShow);

        //}
        [HttpGet("[action]/{courseId:int}")]
        [ProducesResponseType(200, Type = typeof(ViewCourseDetailsDTO))]
        public async Task<ActionResult<APIResponse>> GetCourse(int courseId)
        {

            try
            {

                if (courseId == 0)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }


                Course objCourse = await _courseRepo.GetAsync(u => u.Id == courseId);

                if (objCourse == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<ViewCourseDetailsDTO>(objCourse);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);


            }


            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

           

        }


        [HttpDelete("{courseId:int}", Name ="deleteCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteCourse(int courseId)
        {


            try
            {



                if (courseId == 0)
                {
                    return BadRequest();
                }

                var courseObj = await _courseRepo.GetAsync(u => u.Id == courseId);

                if (courseObj == null)
                {

                    ModelState.AddModelError("", $"Something went wrong when deleting the record {courseObj.CourseTitle}");
                    return StatusCode(500, ModelState);


                }


                await _courseRepo.RemoveAsync(courseObj);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }

        


        [HttpPatch("{courseId:int}", Name = "UpdateCourse")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateCourse(int courseId, [FromBody] EditCourseDTO coursemodel)
        {

            try
            {




                if (coursemodel == null || courseId != coursemodel.Id)
                {
                    return BadRequest(ModelState);
                }


                Course courseObj = _mapper.Map<Course>(coursemodel);

                await _courseRepo.UpdateAsync(courseObj);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }





        //[Route("GetUsers")]
        [HttpGet("[action]/{id:int}")]
        [ProducesResponseType(200, Type = typeof(ViewCourseDetailsDTO))]
        public async Task<ActionResult<APIResponse>> GetUsers(int id)
        {

            try
            {


                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }


                var objList =   _courseRepo.GetUsers(id);

                if (objList == null)
                {
                    return NotFound();
                }


                var objDto = new List<ViewCourseDetailsDTO>();


                foreach (var obj in objList)
                    {
                      objDto.Add(_mapper.Map<ViewCourseDetailsDTO>(obj));
                    }

                 

                _response.Result = objDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

                //if (id == 0)
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;

                //    return BadRequest(_response);
                //}


                //var objList = _courseRepo.GetUsers(id);

                //if (objList == null)
                //{
                //    return NotFound();
                //}

                //var objDto = new List<ViewCourseDetailsDTO>();

                //foreach (var obj in objList)
                //{
                //    objDto.Add(_mapper.Map<ViewCourseDetailsDTO>(obj));
                //}

                ////var objCourse = await _courseRepo.GetUsers(id);


                ////_response.Result = _mapper.Map<ViewCourseDetailsDTO>(objDto);
                //_response.Result = objDto;
                //_response.StatusCode = HttpStatusCode.OK;
                //return Ok(_response);


            }


            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;




            //var objList = _courseRepo.GetUsers(id);

            //if (objList == null)
            //{
            //    return NotFound();
            //}

            //var objToShow = new List<ViewCourseDetailsDTO>();

            //foreach (var obj in objList)
            //{

            //    objToShow.Add(_mapper.Map<ViewCourseDetailsDTO>(obj));

            //}

            //return Ok(objToShow);

        }



    }
}
