using Microsoft.EntityFrameworkCore.Migrations;

namespace OllsMart.Migrations
{
    public partial class userroleqs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Products_ProductId1",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductId1",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductAttributes");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductAttributes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Products_ProductId",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductAttributes",
                type: "text",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId1",
                table: "ProductAttributes",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Products_ProductId1",
                table: "ProductAttributes",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
