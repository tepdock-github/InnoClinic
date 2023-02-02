using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentsService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appoitments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    DoctorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoitments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Complaints = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recomendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppoitmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Appoitments_AppoitmentId",
                        column: x => x.AppoitmentId,
                        principalTable: "Appoitments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Appoitments",
                columns: new[] { "Id", "Date", "DoctorFirstName", "DoctorId", "DoctorLastName", "PatientFirstName", "PatientId", "PatientLastName", "ResultId", "ServiceId", "ServiceName", "Time", "isApproved", "isComplete" },
                values: new object[,]
                {
                    { 1, "20 jan 2022", "Doctor1_testData", 1, "Doctor1_testData", "Patient1_testData", 1, "Patient1_testData", 1, 1, "Service1_testData", "10 am", true, true },
                    { 2, "20 jan 2024", "Doctor2_testData", 2, "Doctor2_testData", "Patient1_testData", 1, "Patient1_testData", null, 1, "Service1_testData", "10 am", false, false },
                    { 3, "21 feb 2023", "Doctor1_testData", 1, "Doctor1_testData", "Patient2_testData", 2, "Patient2_testData", null, 1, "Service1_testData", "10 am", true, false }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Id", "AppoitmentId", "Complaints", "Conclusion", "Diagnosis", "Recomendations" },
                values: new object[] { 1, 1, "complains a lot", "conclusion", null, "pills" });

            migrationBuilder.CreateIndex(
                name: "IX_Results_AppoitmentId",
                table: "Results",
                column: "AppoitmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Appoitments");
        }
    }
}
