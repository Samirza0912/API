using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiPractice.Migrations
{
    public partial class addimageurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 11, 17, 36, 28, 605, DateTimeKind.Local).AddTicks(1347),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 10, 12, 31, 59, 238, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Categories",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 10, 12, 31, 59, 238, DateTimeKind.Local).AddTicks(6754),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2022, 8, 11, 17, 36, 28, 605, DateTimeKind.Local).AddTicks(1347));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
