using ITHSCourseSchoolWEB.Models.DTO.Course;
using AutoMapper;
using ITHSCourseSchoolWEB.Models;

namespace ITHSCourseSchoolWEB
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {

            CreateMap<CreateCourseDTO, Course>().ReverseMap();
            CreateMap<ListUserDTO, Course>().ReverseMap();
            CreateMap<Course, EditCourseDTO>().ReverseMap();
            CreateMap<Course, ViewCourseDetailsDTO>().ReverseMap();


        }


    }
}
