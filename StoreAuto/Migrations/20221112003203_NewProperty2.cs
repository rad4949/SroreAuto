using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAuto.Migrations
{
    public partial class NewProperty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompleteSets_Orders_OrderId",
                table: "CompleteSets");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "CompleteSets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CompleteSets_Orders_OrderId",
                table: "CompleteSets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompleteSets_Orders_OrderId",
                table: "CompleteSets");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "CompleteSets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CompleteSets_Orders_OrderId",
                table: "CompleteSets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
