using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProfilesService.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataDoctorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DoctorsProfiles",
                columns: new[] { "Id", "AccountId", "CareerStartYear", "DateOfBirth", "FirstName", "LastName", "MiddleName", "OfficeId", "SpecializationId", "Status" },
                values: new object[,]
                {
                    { 997, 1, "1", "1", "Hello", "Hello", "Hello", 2, 3, "Remote" },
                    { 998, 4, "1", "1", "Bye", "Bye", "Hello", 2, 4, "At office" },
                    { 1000, 3, "1", "1", "HelloBye", "Bye", "Hello", 3, 4, "At office" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DoctorsProfiles",
                keyColumn: "Id",
                keyValue: 997);

            migrationBuilder.DeleteData(
                table: "DoctorsProfiles",
                keyColumn: "Id",
                keyValue: 998);

            migrationBuilder.DeleteData(
                table: "DoctorsProfiles",
                keyColumn: "Id",
                keyValue: 1000);
        }
    }
}
