﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAuto.Migrations
{
    public partial class NewPropertyConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ColorId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllInvoices",
                table: "AllInvoices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Cars");

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

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "Cars");

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
                name: "PK_AllInvoices",
                table: "AllInvoices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                table: "Cars",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
