using Microsoft.EntityFrameworkCore.Migrations;

namespace OllsMart.Migrations
{
    public partial class expensermearks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Expenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Expenses");
        }
    }
}
