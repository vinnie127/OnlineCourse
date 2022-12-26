using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHSCourseSchool.Migrations
{
    public partial class test123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_LocalUsers_LocalUserId",
                table: "Course");

            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.DropIndex(
                name: "IX_Course_LocalUserId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LocalUserId",
                table: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocalUserId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_LocalUserId",
                table: "Course",
                column: "LocalUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_LocalUsers_LocalUserId",
                table: "Course",
                column: "LocalUserId",
                principalTable: "LocalUsers",
                principalColumn: "Id");
        }
    }
}
