using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EindcaseOefenen.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEdition_Courses_CourseId",
                table: "CourseEdition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEdition",
                table: "CourseEdition");

            migrationBuilder.DropIndex(
                name: "IX_CourseEdition_CourseId",
                table: "CourseEdition");

            migrationBuilder.RenameTable(
                name: "CourseEdition",
                newName: "CourseEditions");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseEditions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEditions",
                table: "CourseEditions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEditions",
                table: "CourseEditions");

            migrationBuilder.RenameTable(
                name: "CourseEditions",
                newName: "CourseEdition");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseEdition",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEdition",
                table: "CourseEdition",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEdition_CourseId",
                table: "CourseEdition",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEdition_Courses_CourseId",
                table: "CourseEdition",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
