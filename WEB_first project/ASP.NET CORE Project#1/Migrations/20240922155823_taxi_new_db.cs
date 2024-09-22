using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_CORE_Project_1.Migrations
{
    /// <inheritdoc />
    public partial class taxi_new_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryRole",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Account",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrivingExperienceYears",
                table: "Account",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Account",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryRole",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "DrivingExperienceYears",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Account");
        }
    }
}
