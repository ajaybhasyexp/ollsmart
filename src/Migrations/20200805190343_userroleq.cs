using Microsoft.EntityFrameworkCore.Migrations;

namespace OllsMart.Migrations
{
    public partial class userroleq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Mrp",
                table: "ProductAttributes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "ProductAttributes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mrp",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "ProductAttributes");
        }
    }
}
