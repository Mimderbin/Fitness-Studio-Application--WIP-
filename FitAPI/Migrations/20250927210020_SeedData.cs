using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Kunden",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Kunden",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { 1, "Admin@example.com", "SuperAdmin", "PLACEHOLDER_HASH" });

            migrationBuilder.InsertData(
                table: "Kunden",
                columns: new[] { "Id", "Email", "MemberSince", "Name", "PasswordHash", "Phone", "SubscriptionValidUntil", "Vorname" },
                values: new object[] { 1, "max@example.com", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mustermann", "PLACEHOLDER_HASH", "123456789", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max" });

            migrationBuilder.CreateIndex(
                name: "IX_Kunden_Email",
                table: "Kunden",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kunden_Email",
                table: "Kunden");

            migrationBuilder.DropIndex(
                name: "IX_Admins_Email",
                table: "Admins");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kunden",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Kunden");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Admins",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Kunden",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
