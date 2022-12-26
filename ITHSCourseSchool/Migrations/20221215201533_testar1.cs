using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITHSCourseSchool.Migrations
{
    public partial class testar1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_LocalUsers_LocalUserId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_LocalUserId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LocalUserId",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "CourseLocalUser",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLocalUser", x => new { x.CoursesCourseId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CourseLocalUser_Course_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseLocalUser_LocalUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "LocalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLocalUser_UsersId",
                table: "CourseLocalUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLocalUser");

            migrationBuilder.AddColumn<int>(
                name: "LocalUserId",
                table: "Course",
                type: "int",
                nullable: true);

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
