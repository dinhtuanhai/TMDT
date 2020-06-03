using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addIDbuyerforOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdBuyer",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdBuyer",
                table: "Orders");
        }
    }
}
