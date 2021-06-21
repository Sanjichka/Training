using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Coach",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Coach",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Coach");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Coach");
        }
    }
}
