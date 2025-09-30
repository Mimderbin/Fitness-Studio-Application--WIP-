using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SubscriptionValidUntil",
                table: "Kunden",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "MemberSince",
                table: "Kunden",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "10000.k5lGr0JPTaQ=.+GVujDgQ1dNYx7ZIl9N5EqhHj/E=");

            migrationBuilder.UpdateData(
                table: "Kunden",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MemberSince", "PasswordHash", "SubscriptionValidUntil" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "password", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubscriptionValidUntil",
                table: "Kunden",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MemberSince",
                table: "Kunden",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "PLACEHOLDER_HASH");

            migrationBuilder.UpdateData(
                table: "Kunden",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MemberSince", "PasswordHash", "SubscriptionValidUntil" },
                values: new object[] { new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PLACEHOLDER_HASH", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
