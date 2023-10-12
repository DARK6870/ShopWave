using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class ni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductVariationVariationId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductVariationVariationId",
                table: "Order",
                column: "ProductVariationVariationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ProductVariation_ProductVariationVariationId",
                table: "Order",
                column: "ProductVariationVariationId",
                principalTable: "ProductVariation",
                principalColumn: "VariationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ProductVariation_ProductVariationVariationId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ProductVariationVariationId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ProductVariationVariationId",
                table: "Order");
        }
    }
}
