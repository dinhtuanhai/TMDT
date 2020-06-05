using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addQuantityInBakery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Bakery",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Bakery");
        }
    }
}
