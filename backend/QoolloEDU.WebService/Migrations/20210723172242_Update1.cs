using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QoolloEDU.WebService.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                schema: "QoolloEDU",
                table: "Developer");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "QoolloEDU",
                table: "Request",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "QoolloEDU",
                table: "Developer",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "QoolloEDU",
                table: "Developer");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "QoolloEDU",
                table: "Request",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "QoolloEDU",
                table: "Developer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
