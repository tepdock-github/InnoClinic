using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentsService.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appoitments",
                columns: new[] { "Id", "Date", "DoctorId", "PatientId", "ResultId", "ServiceId", "Time", "isApproved", "isComplete" },
                values: new object[,]
                {
                    { "24f001f6-8ce6-4f8e-874d-9baa095ce13c", "12.01.2023", "204", "123", null, "123", "12am", true, true },
                    { "7b0463b0-379e-469a-9e33-452f1bb33be6", "13.01.2023", "123", "204", null, "123", "1pm", false, false },
                    { "f4559d08-1e40-495d-ae12-fe95efae1fa2", "13.01.2023", "123", "123", null, "123", "10am", true, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appoitments",
                keyColumn: "Id",
                keyValue: "24f001f6-8ce6-4f8e-874d-9baa095ce13c");

            migrationBuilder.DeleteData(
                table: "Appoitments",
                keyColumn: "Id",
                keyValue: "7b0463b0-379e-469a-9e33-452f1bb33be6");

            migrationBuilder.DeleteData(
                table: "Appoitments",
                keyColumn: "Id",
                keyValue: "f4559d08-1e40-495d-ae12-fe95efae1fa2");
        }
    }
}
