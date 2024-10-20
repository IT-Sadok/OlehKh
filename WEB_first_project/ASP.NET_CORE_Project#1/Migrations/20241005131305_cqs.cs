using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_CORE_Project_1.Migrations
{
    /// <inheritdoc />
    public partial class cqs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_UserId",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_UserId",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");
        }
    }
}
