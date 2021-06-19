using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discipline_Training_TrainingID",
                table: "Discipline");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_Coach_CoachID",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Discipline_Training_TrainingID",
                table: "Discipline",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Coach_CoachID",
                table: "Training",
                column: "CoachID",
                principalTable: "Coach",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discipline_Training_TrainingID",
                table: "Discipline");

            migrationBuilder.DropForeignKey(
                name: "FK_Training_Coach_CoachID",
                table: "Training");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Discipline_Training_TrainingID",
                table: "Discipline",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_Coach_CoachID",
                table: "Training",
                column: "CoachID",
                principalTable: "Coach",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
