using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMS.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "SupplyPrice");

            migrationBuilder.AddColumn<int>(
                name: "LowQuantityAmount",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityInStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowQuantityAmount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "SupplyPrice",
                table: "Product",
                newName: "Price");
        }
    }
}
