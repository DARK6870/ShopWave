using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class nui : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_AppUserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ProductId1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Review");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Review",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_AppUserId1",
                table: "Review",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_AppUserId",
                table: "Review",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_AppUserId1",
                table: "Review",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_AppUserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_AppUserId1",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_AppUserId1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Review");

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
                name: "FK_Review_AspNetUsers_AppUserId",
                table: "Review",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId1",
                table: "Review",
                column: "ProductId1",
                principalTable: "Product",
                principalColumn: "ProductId");
        }
    }
}
