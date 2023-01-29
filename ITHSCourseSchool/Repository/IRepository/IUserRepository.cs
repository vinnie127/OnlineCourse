using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.User;

namespace ITHSCourseSchool.Repository.IRepository
{
    public interface IUserRepository
    {

        bool IsUniqueUser(string username);
        //Vi får LoginResponse tillbaka, lägger in Request
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        //Vi returnerar user
        Task<UserModelDTO> Register(RegistrationRequestDTO registerationRequestDTO);
        public ICollection<ApplicationUser> GetCourses(string studentId);
        //public bool AddCourse(CourseToAddDTO model);
        public  Task<ApplicationUser> AddCourse(CourseToAddDTO model);
        public ICollection<ApplicationUser> GetUsers();
        public  Task<ApplicationUser> RemoveCourse(CourseToAddDTO model);
        public bool Save();
        //public ICollection<LocalUser> GetStudents();
        //public ICollection<LocalUser> GetTeachers();

    }
}
