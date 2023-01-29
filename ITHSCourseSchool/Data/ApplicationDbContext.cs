
using ITHSCourseSchool.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ITHSCourseSchool.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }

      

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Course> Course { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }
    }
}
