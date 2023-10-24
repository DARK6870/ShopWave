using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirtName",
                table: "UserData",
                newName: "FirtsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirtsName",
                table: "UserData",
                newName: "FirtName");
        }
    }
}
