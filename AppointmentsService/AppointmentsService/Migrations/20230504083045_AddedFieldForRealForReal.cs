using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsService.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldForRealForReal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorFirstName",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorLastName",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFirstName",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DoctorLastName",
                table: "Schedules");
        }
    }
}
