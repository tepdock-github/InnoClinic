using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProfilesService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorsProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    SpecializationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    CareerStartYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLinkedToAccount = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionistProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionistProfiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DoctorsProfiles",
                columns: new[] { "Id", "AccountId", "CareerStartYear", "DateOfBirth", "FirstName", "LastName", "MiddleName", "OfficeId", "SpecializationId", "SpecializationName", "Status" },
                values: new object[,]
                {
                    { 997, 1, "1", "1", "Hello", "Hello", "Hello", 2, 3, "Hello", "Remote" },
                    { 998, 4, "1", "1", "Bye", "Bye", "Hello", 2, 4, "spec4", "At office" },
                    { 1000, 3, "1", "1", "HelloBye", "Bye", "Hello", 3, 4, "spec4", "At office" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsProfiles");

            migrationBuilder.DropTable(
                name: "PatientProfiles");

            migrationBuilder.DropTable(
                name: "ReceptionistProfiles");
        }
    }
}
