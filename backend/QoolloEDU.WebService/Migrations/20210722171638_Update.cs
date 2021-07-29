using Microsoft.EntityFrameworkCore.Migrations;

namespace QoolloEDU.WebService.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Link_Developer_DeveloperId",
                schema: "QoolloEDU",
                table: "Link");

            migrationBuilder.DropIndex(
                name: "IX_Link_DeveloperId",
                schema: "QoolloEDU",
                table: "Link");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                schema: "QoolloEDU",
                table: "Link");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "QoolloEDU",
                table: "Link");

            migrationBuilder.AlterColumn<int>(
                name: "Place",
                schema: "QoolloEDU",
                table: "Project",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BaseUserId",
                schema: "QoolloEDU",
                table: "Link",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Place",
                schema: "QoolloEDU",
                table: "Project",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BaseUserId",
                schema: "QoolloEDU",
                table: "Link",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                schema: "QoolloEDU",
                table: "Link",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "QoolloEDU",
                table: "Link",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Link_DeveloperId",
                schema: "QoolloEDU",
                table: "Link",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Developer_DeveloperId",
                schema: "QoolloEDU",
                table: "Link",
                column: "DeveloperId",
                principalSchema: "QoolloEDU",
                principalTable: "Developer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
