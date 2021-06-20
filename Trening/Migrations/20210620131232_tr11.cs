using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discipline_Training_TrainingID",
                table: "Discipline");

            migrationBuilder.DropIndex(
                name: "IX_Discipline_TrainingID",
                table: "Discipline");

            migrationBuilder.DropColumn(
                name: "TrainingID",
                table: "Discipline");

            migrationBuilder.AddColumn<string>(
                name: "Discipline",
                table: "Training",
                maxLength: 28,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "Training");

            migrationBuilder.AddColumn<int>(
                name: "TrainingID",
                table: "Discipline",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_TrainingID",
                table: "Discipline",
                column: "TrainingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Discipline_Training_TrainingID",
                table: "Discipline",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID");
        }
    }
}
