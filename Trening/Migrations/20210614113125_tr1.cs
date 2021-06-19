using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trening.Migrations
{
    public partial class tr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    ExerciseRank = table.Column<string>(maxLength: 20, nullable: true),
                    Awards = table.Column<string>(maxLength: 50, nullable: true),
                    Certificates = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingName = table.Column<string>(maxLength: 50, nullable: false),
                    Platform = table.Column<string>(maxLength: 30, nullable: false),
                    LinkPlatform = table.Column<string>(maxLength: 100, nullable: true),
                    CompanyCoache = table.Column<string>(maxLength: 30, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    NumClMonth = table.Column<int>(nullable: false),
                    CoachID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Training_Coach_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coach",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineName = table.Column<string>(maxLength: 20, nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Type = table.Column<string>(maxLength: 10, nullable: false),
                    Equipment = table.Column<string>(maxLength: 100, nullable: false),
                    Ground = table.Column<string>(maxLength: 28, nullable: false),
                    TrainingID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Discipline_Training_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "Training",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Embg = table.Column<string>(maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 25, nullable: true),
                    EnrollmentDate = table.Column<DateTime>(nullable: true),
                    ExerciseLevel = table.Column<string>(maxLength: 25, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: false),
                    TrainingID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Training_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "Training",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_TrainingID",
                table: "Discipline",
                column: "TrainingID");

            migrationBuilder.CreateIndex(
                name: "IX_Training_CoachID",
                table: "Training",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_User_TrainingID",
                table: "User",
                column: "TrainingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Coach");
        }
    }
}
