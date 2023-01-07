using AutoMapper;
using ITHSCourseSchool.Data;
using ITHSCourseSchool.Models;
using ITHSCourseSchool.Models.DTO.User;
using ITHSCourseSchool.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITHSCourseSchool.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;



        public UserRepository(ApplicationDbContext db, IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper;
            _userManager = userManager; 
           _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            
            var user = _db.ApplicationUsers.FirstOrDefault(x=>x.UserName == username);
            if (user == null)
            {


                return true;
            }

            return false;

        }

        public ApplicationUser GetUser(string userName)
        {
            return _db.ApplicationUsers.FirstOrDefault(a => a.Id == userName);
        }

        public ApplicationUser GetUserId(string UserId)
        {
            return _db.ApplicationUsers.FirstOrDefault(a => a.Id == UserId);
        }


        public ICollection<ApplicationUser> GetUsers()
        {
            return _db.ApplicationUsers.OrderBy(a => a.Name).ToList();

        }

        //public  ICollection<ApplicationUser> GetStudents()
        //{

        //    return _db.ApplicationUsers.OrderBy(a => a.Name).Where(a=>a.Role == "student").ToList();

            

        //}

        //public ICollection<LocalUser> GetTeachers()
        //{

        //    return _db.LocalUsers.OrderBy(a => a.Name).Where(a => a.Role == "teacher").ToList();


        //}


        //vi måste skapa en token för att validera user och skicka tillbaka 
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            
            var user = _db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
           
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);


            
            if (user == null || isValid == false)
            {

                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null

                };


            }


            //if user was found, generate JWT token. Skapa secretkey som ska validera om token är ok eller inte 
            var roles = await _userManager.GetRolesAsync(user);
            //generate the securityToken
            var tokenHandler = new JwtSecurityTokenHandler();
            //encode vår secret key för att komma åt den
            var key = Encoding.ASCII.GetBytes(secretKey);
            //token descriptor = contains everything , like claims, roles, etc...
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //Vi skapar en egen claim. Vi använder Id för att identifiera Name.
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };
            //Så allt ovan, TokenDescriptor, Den definierar vad vår token ska innehålla. hur man encryptar , expires etc..
            
            //Nu skapar vi vår token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserModelDTO>(user),
               

            };

            return loginResponseDTO;

        }



        
        public async Task<UserModelDTO> Register(RegistrationRequestDTO registerationRequestDTO)
        {

            ApplicationUser user = new ()
            {
                UserName = registerationRequestDTO.UserName.ToUpper(),
                Name = registerationRequestDTO.Name,
               
            };


            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);
                if (result.Succeeded)
                {

                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("student"));
                        await _roleManager.CreateAsync(new IdentityRole("teacher"));
                    }

                    if (!_roleManager.RoleExistsAsync(registerationRequestDTO.Role).GetAwaiter().GetResult())
                    {
                       return null;

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, registerationRequestDTO.Role);

                        var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registerationRequestDTO.UserName);

                        return _mapper.Map<UserModelDTO>(userToReturn);
                    }

                 

                }

                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {

            }


            // _db.LocalUsers.Add(user);   
            //await  _db.SaveChangesAsync();
            // user.Password = "";

            return new UserModelDTO();
        }


        public ICollection<ApplicationUser> GetCourses(string userName)
        {



            return _db.ApplicationUsers.Where(c => c.UserName == userName).Include(a => a.Courses).ToList();
            
        }

        

        public async Task<ApplicationUser> AddCourse(CourseToAddDTO model)
        {

            
            Course courseObj = _db.Course.FirstOrDefault(u => u.Id == model.CourseId);

            var userObj = GetUser(model.UserId);



            if (courseObj.Id != null && userObj.Id != null)
            {

                userObj.Courses.Add(courseObj);
                Save();
                return userObj;

            }

            else
            {
                return null;
            }


            //var userObj = GetUser(model.UserId);

            //Course CourseObj = _db.Course.FirstOrDefault(a=>a.Id == model.CourseId);
            //userObj.Courses.Add(CourseObj);

            //return Save();

        }




        //public  bool AddCourse(CourseToAddDTO model)
        //{



        //    //var userToReturn =  _db.ApplicationUsers.FirstOrDefault(u => u.Id == model.UserId);

        //    //var courseObj = _db.Course.FirstOrDefault(u=>u.Id == model.CourseId);

        //    Course courseObj =  _db.Course.FirstOrDefault(u => u.Id == model.CourseId);

        //    var userObj = GetUser(model.UserId);



        //    if(courseObj.Id != null && userObj.Id != null)
        //    {

        //        userObj.Courses.Add(courseObj);

        //        return Save();

        //    }

        //    else
        //    {
        //        return false;
        //    }


        //    //var userObj = GetUser(model.UserId);

        //    //Course CourseObj = _db.Course.FirstOrDefault(a=>a.Id == model.CourseId);
        //    //userObj.Courses.Add(CourseObj);

        //    //return Save();

        //}

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

   
    }
}
