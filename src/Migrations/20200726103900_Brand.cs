using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace OllsMart.Migrations
{
    public partial class Brand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BrandName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "CategoryId");
        }
    }
}
