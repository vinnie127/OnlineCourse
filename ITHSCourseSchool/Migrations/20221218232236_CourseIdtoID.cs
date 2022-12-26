using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHSCourseSchool.Migrations
{
    public partial class CourseIdtoID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLocalUser_Course_CoursesCourseId",
                table: "CourseLocalUser");

            migrationBuilder.RenameColumn(
                name: "CoursesCourseId",
                table: "CourseLocalUser",
                newName: "CoursesId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Course",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLocalUser_Course_CoursesId",
                table: "CourseLocalUser",
                column: "CoursesId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLocalUser_Course_CoursesId",
                table: "CourseLocalUser");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "CourseLocalUser",
                newName: "CoursesCourseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Course",
                newName: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLocalUser_Course_CoursesCourseId",
                table: "CourseLocalUser",
                column: "CoursesCourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
