using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electro_goods_API.Migrations
{
    /// <inheritdoc />
    public partial class addMoreLang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameUA",
                table: "Manufacturers",
                newName: "NameUK");

            migrationBuilder.RenameColumn(
                name: "NameRU",
                table: "Manufacturers",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ProductNameUK",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNameUK",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "NameUK",
                table: "Manufacturers",
                newName: "NameUA");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Manufacturers",
                newName: "NameRU");
        }
    }
}
