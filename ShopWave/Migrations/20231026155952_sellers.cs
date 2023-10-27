using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopWave.Migrations
{
    /// <inheritdoc />
    public partial class sellers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "SellerData");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBrth",
                table: "SellerData",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PassEDate",
                table: "SellerData",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PassNr",
                table: "SellerData",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PassSDate",
                table: "SellerData",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBrth",
                table: "SellerData");

            migrationBuilder.DropColumn(
                name: "PassEDate",
                table: "SellerData");

            migrationBuilder.DropColumn(
                name: "PassNr",
                table: "SellerData");

            migrationBuilder.DropColumn(
                name: "PassSDate",
                table: "SellerData");

            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "SellerData",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }
    }
}
