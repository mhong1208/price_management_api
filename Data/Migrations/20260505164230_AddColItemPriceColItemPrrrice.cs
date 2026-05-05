using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace price_management_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColItemPriceColItemPrrrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Items_ItemId1",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Suppliers_SupplierId1",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_ItemId1",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_SupplierId1",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "SupplierId1",
                table: "ItemPrices");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "ItemPrices",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ItemId",
                table: "ItemPrices",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ItemPrices",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_ItemId",
                table: "ItemPrices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_SupplierId",
                table: "ItemPrices",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Items_ItemId",
                table: "ItemPrices",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Suppliers_SupplierId",
                table: "ItemPrices",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Items_ItemId",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Suppliers_SupplierId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_ItemId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_SupplierId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ItemPrices");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "ItemPrices",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "ItemPrices",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId1",
                table: "ItemPrices",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId1",
                table: "ItemPrices",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_ItemId1",
                table: "ItemPrices",
                column: "ItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_SupplierId1",
                table: "ItemPrices",
                column: "SupplierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Items_ItemId1",
                table: "ItemPrices",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Suppliers_SupplierId1",
                table: "ItemPrices",
                column: "SupplierId1",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
