using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsService.Migrations
{
    /// <inheritdoc />
    public partial class AddSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Appoitments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Appoitments");
        }
    }
}
