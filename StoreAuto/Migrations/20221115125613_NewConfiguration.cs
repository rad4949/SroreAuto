using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAuto.Migrations
{
    public partial class NewConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars");

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
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ColorId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CarId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Cars");

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

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CompleteSets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 25000m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Colors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NameCode",
                table: "Colors",
                columns: new[] { "ColorName", "ColorCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "AllInvoices",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "Price",
                table: "CompleteSets",
                sql: "Price > 10000 AND Price < 99999999");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorName_ColorCode",
                table: "Cars",
                columns: new[] { "ColorName", "ColorCode" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorName_ColorCode",
                table: "Cars",
                columns: new[] { "ColorName", "ColorCode" },
                principalTable: "Colors",
                principalColumns: new[] { "ColorName", "ColorCode" },
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorName_ColorCode",
                table: "Cars");

            migrationBuilder.DropCheckConstraint(
                name: "Price",
                table: "CompleteSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NameCode",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ColorName_ColorCode",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "AllInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AllInvoices_VIN_Number",
                table: "AllInvoices");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "Cars");

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

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CompleteSets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 25000m);

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                table: "Cars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CarId",
                table: "Invoices",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
