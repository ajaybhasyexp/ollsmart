using Microsoft.EntityFrameworkCore.Migrations;

namespace OllsMart.Migrations
{
    public partial class expensea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseHeads_ExpenseHeadId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_ExpenseHeadId",
                table: "Expenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseHeadId",
                table: "Expenses",
                column: "ExpenseHeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseHeads_ExpenseHeadId",
                table: "Expenses",
                column: "ExpenseHeadId",
                principalTable: "ExpenseHeads",
                principalColumn: "ExpenseHeadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
