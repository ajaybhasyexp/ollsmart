using Microsoft.EntityFrameworkCore.Migrations;

namespace OllsMart.Migrations
{
    public partial class order10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalDiscount",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "OrderHeaders");
        }
    }
}
