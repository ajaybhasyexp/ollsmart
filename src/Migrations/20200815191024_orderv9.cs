using Microsoft.EntityFrameworkCore.Migrations;

namespace OllsMart.Migrations
{
    public partial class orderv9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "OrderHeaders",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComputedColumnSql: "'SO'+CONVERT([nvarchar](50),[OrderHeaderId])");

            migrationBuilder.AddColumn<int>(
                name: "SourceId",
                table: "OrderHeaders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "OrderHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "OrderHeaders",
                type: "text",
                nullable: true,
                computedColumnSql: "'SO'+CONVERT([nvarchar](50),[OrderHeaderId])",
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
