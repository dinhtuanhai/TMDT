using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BakeryType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakeryType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 11, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bakery",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDType = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: true),
                    Rating = table.Column<double>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakery", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Bakery__IDType__3E52440B",
                        column: x => x.IDType,
                        principalTable: "BakeryType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 150, nullable: true),
                    Password = table.Column<string>(maxLength: 150, nullable: true),
                    IDCustomer = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Account__IDCusto__398D8EEE",
                        column: x => x.IDCustomer,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCustomer = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Orders__IDCustom__412EB0B6",
                        column: x => x.IDCustomer,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(nullable: false),
                    IDBakery = table.Column<int>(nullable: true),
                    IDShoppingCart = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK__ShoppingCartItem__IDShoppingCart",
                        column: x => x.IDShoppingCart,
                        principalTable: "ShoppingCart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ShoppingCartItem__IDBak",
                        column: x => x.IDBakery,
                        principalTable: "Bakery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    IDOrder = table.Column<int>(nullable: false),
                    IDBakery = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: true),
                    Total = table.Column<long>(nullable: true),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orderdetail_pk", x => new { x.IDOrder, x.IDBakery });
                    table.ForeignKey(
                        name: "FK__OrderDeta__IDBak__44FF419A",
                        column: x => x.IDBakery,
                        principalTable: "Bakery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__OrderDeta__IDOrd__440B1D61",
                        column: x => x.IDOrder,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_IDCustomer",
                table: "Account",
                column: "IDCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Bakery_IDType",
                table: "Bakery",
                column: "IDType");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_IDBakery",
                table: "OrderDetail",
                column: "IDBakery");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IDCustomer",
                table: "Orders",
                column: "IDCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_IDShoppingCart",
                table: "ShoppingCartItem",
                column: "IDShoppingCart");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_IDBakery",
                table: "ShoppingCartItem",
                column: "IDBakery");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ShoppingCartItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "Bakery");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "BakeryType");
        }
    }
}
