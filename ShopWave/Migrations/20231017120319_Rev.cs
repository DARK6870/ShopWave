using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class Rev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId1",
                table: "Review",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId1",
                table: "Review",
                column: "ProductId1",
                principalTable: "Product",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ProductId1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Review");
        }
    }
}
