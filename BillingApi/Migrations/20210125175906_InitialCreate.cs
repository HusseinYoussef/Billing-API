using Microsoft.EntityFrameworkCore.Migrations;

namespace BillingApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Discount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 1, 10, "USA", "T-Shirt", 200.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 2, 5, "CA", "Pants", 150.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 3, 0, "China", "Hat", 50.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 4, 10, "UK", "Shoes", 100.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 5, 15, "USA", "Suit", 1000.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 6, 10, "USA", "Jacket", 300.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discount", "Manufacturer", "Name", "Price" },
                values: new object[] { 7, 15, "USA", "Dress", 500.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
