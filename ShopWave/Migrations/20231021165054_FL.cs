using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class FL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIO",
                table: "UserData");

            migrationBuilder.AddColumn<string>(
                name: "FirtName",
                table: "UserData",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserData",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirtName",
                table: "UserData");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserData");

            migrationBuilder.AddColumn<string>(
                name: "FIO",
                table: "UserData",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }
    }
}
