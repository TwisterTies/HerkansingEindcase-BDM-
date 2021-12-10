using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EindcaseOefenen.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseEditions_CourseId",
                table: "CourseEditions",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEditions_Courses_CourseId",
                table: "CourseEditions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEditions_Courses_CourseId",
                table: "CourseEditions");

            migrationBuilder.DropIndex(
                name: "IX_CourseEditions_CourseId",
                table: "CourseEditions");
        }
    }
}
