using AutoMapper;
using ITHSCourseSchool.Models.DTO.User;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.Course;

namespace ITHSCourseSchool
{
    public class MappingConfig : Profile
    {


        public MappingConfig()
        {
            
            CreateMap<Course, RegisterCourseDTO>().ReverseMap();
            CreateMap<Course, ViewCourseDetailsDTO>().ReverseMap();
            CreateMap<Course, ViewCourse>().ReverseMap();
            CreateMap<Course, EditCourseDTO>().ReverseMap();
            CreateMap<Course, CourseInfoClient>().ReverseMap();
          
            //CreateMap<LocalUser, UserDTO>().ReverseMap();
            //CreateMap<LocalUser, LoginRequestDTO>().ReverseMap();
            //CreateMap<LocalUser, LoginResponseDTO>().ReverseMap();
            //CreateMap<LocalUser, RegistrationRequestDTO>().ReverseMap();

            CreateMap<ApplicationUser, LoginRequestDTO>().ReverseMap();
            CreateMap<ApplicationUser, LoginResponseDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegistrationRequestDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserModelDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserViewOnly>().ReverseMap();
            CreateMap<ApplicationUser, CoursesInUser>().ReverseMap();
            CreateMap<ApplicationUser, CoursesInUser>().ReverseMap();
        }

 


    }
}
