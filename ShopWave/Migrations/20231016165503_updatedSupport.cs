using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class updatedSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Support");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Support",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Support",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Support");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Support");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Support",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
