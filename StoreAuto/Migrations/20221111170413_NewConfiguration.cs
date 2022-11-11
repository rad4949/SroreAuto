using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAuto.Migrations
{
    public partial class NewConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Cars_CarId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Orders_OrderId",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CarId",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "AllInvoices");

            migrationBuilder.RenameColumn(
                name: "ModelName",
                table: "Models",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "AllInvoices",
                newName: "VIN_Number");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_OrderId",
                table: "AllInvoices",
                newName: "IX_AllInvoices_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_ClientId",
                table: "AllInvoices",
                newName: "IX_AllInvoices_ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Models",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllInvoices",
                table: "AllInvoices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AllInvoices_VIN_Number",
                table: "AllInvoices",
                column: "VIN_Number",
                unique: true,
                filter: "[VIN_Number] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AllInvoices_Cars_VIN_Number",
                table: "AllInvoices",
                column: "VIN_Number",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllInvoices_Clients_ClientId",
                table: "AllInvoices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AllInvoices_Orders_OrderId",
                table: "AllInvoices",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllInvoices_Cars_VIN_Number",
                table: "AllInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_AllInvoices_Clients_ClientId",
                table: "AllInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_AllInvoices_Orders_OrderId",
                table: "AllInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllInvoices",
                table: "AllInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AllInvoices_VIN_Number",
                table: "AllInvoices");

            migrationBuilder.RenameTable(
                name: "AllInvoices",
                newName: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Models",
                newName: "ModelName");

            migrationBuilder.RenameColumn(
                name: "VIN_Number",
                table: "Invoices",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_AllInvoices_OrderId",
                table: "Invoices",
                newName: "IX_Invoices_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_AllInvoices_ClientId",
                table: "Invoices",
                newName: "IX_Invoices_ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "ModelName",
                table: "Models",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CarId",
                table: "Invoices",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Cars_CarId",
                table: "Invoices",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Orders_OrderId",
                table: "Invoices",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
