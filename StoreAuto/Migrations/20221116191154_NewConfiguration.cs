using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAuto.Migrations
{
    public partial class NewConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AvailabilityCars_AvailabilityCarId",
                table: "Cars");

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
                name: "IX_Cars_AvailabilityCarId",
                table: "Cars");

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
                name: "InvoiceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "AvailabilityCarId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
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

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName", "Country" },
                values: new object[,]
                {
                    { 1, "Mercedes-Benz", "Germany" },
                    { 2, "Audi", "Germany" },
                    { 3, "Mercedes-Benz", "Germany" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "Igor", "Radchuk", 665001701 },
                    { 2, "Nazar", "Shevchuk", 675001705 },
                    { 3, "Petro", "Romuniuk", 678801788 }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorCode", "ColorName" },
                values: new object[,]
                {
                    { "12qw", "Black" },
                    { "1111", "White" },
                    { "1rr1", "Black" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DateOrder", "Term" },
                values: new object[] { 1, new DateTime(2022, 11, 16, 21, 11, 53, 862, DateTimeKind.Local).AddTicks(936), new DateTime(2022, 12, 6, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "StorageAuto",
                columns: new[] { "Id", "ClientAddress" },
                values: new object[,]
                {
                    { 1, "Shevcenka 5" },
                    { 2, "Konovaltsa 8" },
                    { 3, "Dachna 11" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BodyType", "BrandId", "Name" },
                values: new object[] { 1, "Crossover", 1, "GLS" });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BodyType", "BrandId", "Name" },
                values: new object[] { 2, "Universal", 2, "A6" });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BodyType", "BrandId", "Name" },
                values: new object[] { 3, "Sedan", 3, "CLC" });

            migrationBuilder.InsertData(
                table: "CompleteSets",
                columns: new[] { "Id", "EngineVolume", "FuelType", "ModelId", "ModelYear", "OrderId", "Price" },
                values: new object[] { 1, 3, "Gasoline", 1, 2021, null, 250000m });

            migrationBuilder.InsertData(
                table: "CompleteSets",
                columns: new[] { "Id", "EngineVolume", "FuelType", "ModelId", "ModelYear", "OrderId", "Price" },
                values: new object[] { 2, 2, "Gasoline", 2, 2020, 1, 200000m });

            migrationBuilder.InsertData(
                table: "CompleteSets",
                columns: new[] { "Id", "EngineVolume", "FuelType", "ModelId", "ModelYear", "OrderId", "Price" },
                values: new object[] { 3, 3, "Gasoline", 3, 2020, null, 210000m });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ColorCode", "ColorName", "CompleteSetId", "IsCash" },
                values: new object[] { 1, "12qw", "Black", 1, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ColorCode", "ColorName", "CompleteSetId", "IsCash" },
                values: new object[] { 2, "1111", "White", 2, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ColorCode", "ColorName", "CompleteSetId", "IsCash" },
                values: new object[] { 3, "1rr1", "Black", 3, false });

            migrationBuilder.InsertData(
                table: "AllInvoices",
                columns: new[] { "Id", "VIN_Number", "ClientId", "Date", "OrderId" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2022, 11, 16, 21, 11, 53, 860, DateTimeKind.Local).AddTicks(4733), null },
                    { 2, 2, 2, new DateTime(2022, 11, 16, 21, 11, 53, 862, DateTimeKind.Local).AddTicks(236), 1 },
                    { 3, 3, 3, new DateTime(2022, 11, 16, 21, 11, 53, 862, DateTimeKind.Local).AddTicks(257), null }
                });

            migrationBuilder.InsertData(
                table: "AvailabilityCars",
                columns: new[] { "Id", "CarId", "StorageId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.AddCheckConstraint(
                name: "Price",
                table: "CompleteSets",
                sql: "Price > 10000 AND Price < 99999999");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorName_ColorCode",
                table: "Cars",
                columns: new[] { "ColorName", "ColorCode" });

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityCars_CarId",
                table: "AvailabilityCars",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

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
                name: "FK_AvailabilityCars_Cars_CarId",
                table: "AvailabilityCars",
                column: "CarId",
                principalTable: "Cars",
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
                name: "FK_AvailabilityCars_Cars_CarId",
                table: "AvailabilityCars");

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

            migrationBuilder.DropIndex(
                name: "IX_AvailabilityCars_CarId",
                table: "AvailabilityCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "AllInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AllInvoices_VIN_Number",
                table: "AllInvoices");

            migrationBuilder.DeleteData(
                table: "AllInvoices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AllInvoices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AllInvoices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AvailabilityCars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AvailabilityCars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AvailabilityCars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StorageAuto",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StorageAuto",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StorageAuto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumns: new[] { "ColorCode", "ColorName" },
                keyValues: new object[] { "12qw", "Black" });

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumns: new[] { "ColorCode", "ColorName" },
                keyValues: new object[] { "1rr1", "Black" });

            migrationBuilder.DeleteData(
                table: "Colors",
                keyColumns: new[] { "ColorCode", "ColorName" },
                keyValues: new object[] { "1111", "White" });

            migrationBuilder.DeleteData(
                table: "CompleteSets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CompleteSets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CompleteSets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "AvailabilityCarId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
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
                name: "IX_Cars_AvailabilityCarId",
                table: "Cars",
                column: "AvailabilityCarId",
                unique: true);

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
                name: "FK_Cars_AvailabilityCars_AvailabilityCarId",
                table: "Cars",
                column: "AvailabilityCarId",
                principalTable: "AvailabilityCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
