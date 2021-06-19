using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Coach");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Coach",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
