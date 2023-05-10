using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsService.Migrations
{
    /// <inheritdoc />
    public partial class AddfieldAppoitmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppoitmentId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppoitmentId",
                table: "Schedules");
        }
    }
}
