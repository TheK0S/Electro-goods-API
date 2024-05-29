using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Electro_goods_API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.AddColumn<string>(
           name: "ShippingCity",
           table: "Orders",
           nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "ShippingCity",
            table: "Orders");
        }
    }
}
