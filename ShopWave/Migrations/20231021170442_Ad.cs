using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class Ad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "UserData");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserData",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserData");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "UserData",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
