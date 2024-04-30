using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electro_goods_API.Migrations
{
    /// <inheritdoc />
    public partial class addImgForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributs_Products_ProductDtoId",
                table: "ProductAttributs");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributs_ProductDtoId",
                table: "ProductAttributs");

            migrationBuilder.DropColumn(
                name: "ProductDtoId",
                table: "ProductAttributs");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributs_ProductId",
                table: "ProductAttributs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributs_Products_ProductId",
                table: "ProductAttributs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributs_Products_ProductId",
                table: "ProductAttributs");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributs_ProductId",
                table: "ProductAttributs");

            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductDtoId",
                table: "ProductAttributs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributs_ProductDtoId",
                table: "ProductAttributs",
                column: "ProductDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributs_Products_ProductDtoId",
                table: "ProductAttributs",
                column: "ProductDtoId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
