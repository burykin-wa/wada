using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class change_HireDate_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "HireDate",
                table: "Employees",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HireDate",
                value: new DateOnly(2015, 1, 10));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "HireDate",
                value: new DateOnly(2012, 3, 15));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "HireDate",
                value: new DateOnly(2018, 5, 20));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "HireDate",
                value: new DateOnly(2017, 6, 10));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "HireDate",
                value: new DateOnly(2019, 7, 22));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "HireDate",
                value: new DateOnly(2016, 9, 15));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "HireDate",
                value: new DateOnly(2020, 8, 1));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "HireDate",
                value: new DateOnly(2021, 11, 11));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "HireDate",
                value: new DateOnly(2018, 12, 5));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "HireDate",
                value: new DateOnly(2014, 2, 25));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11L,
                column: "HireDate",
                value: new DateOnly(2013, 4, 17));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12L,
                column: "HireDate",
                value: new DateOnly(2022, 5, 19));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "HireDate" },
                values: new object[] { new DateTime(2024, 6, 21, 12, 15, 51, 821, DateTimeKind.Utc).AddTicks(2), new DateOnly(2019, 10, 30) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HireDate",
                value: new DateTime(2015, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "HireDate",
                value: new DateTime(2012, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "HireDate",
                value: new DateTime(2018, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "HireDate",
                value: new DateTime(2017, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "HireDate",
                value: new DateTime(2019, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "HireDate",
                value: new DateTime(2016, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "HireDate",
                value: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "HireDate",
                value: new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "HireDate",
                value: new DateTime(2018, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "HireDate",
                value: new DateTime(2014, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11L,
                column: "HireDate",
                value: new DateTime(2013, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12L,
                column: "HireDate",
                value: new DateTime(2022, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "HireDate" },
                values: new object[] { new DateTime(2024, 6, 20, 11, 6, 44, 137, DateTimeKind.Utc).AddTicks(8946), new DateTime(2019, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
