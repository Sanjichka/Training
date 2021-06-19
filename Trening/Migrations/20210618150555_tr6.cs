using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Training_TrainingID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_TrainingID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TrainingID",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_TrainingID",
                table: "User",
                column: "TrainingID");

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
