using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID");
        }
    }
}
