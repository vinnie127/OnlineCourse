using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHSCourseSchool.Migrations
{
    public partial class UsercontrollerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LocalUsers");
        }
    }
}
