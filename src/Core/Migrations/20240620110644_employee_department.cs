using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class employee_department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FullTime",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1L, "Human Resources", false, "HR" },
                    { 2L, "Information Technology", false, "IT" }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "About", "DepartmentId", "FullTime", "HireDate" },
                values: new object[] { null, 1L, true, new DateTime(2015, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "About", "DepartmentId", "FullTime", "HireDate", "Position", "SupervisorId" },
                values: new object[] { null, 2L, true, new DateTime(2012, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Senior Manager", null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "About", "DepartmentId", "FirstName", "FullTime", "HireDate", "IsDeleted", "LastName", "Position", "SupervisorId" },
                values: new object[,]
                {
                    { 3L, null, 1L, "Alice", true, new DateTime(2018, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), false, "Johnson", "HR Specialist", 1L },
                    { 4L, null, 2L, "Bob", true, new DateTime(2017, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), false, "Brown", "IT Specialist", 2L },
                    { 5L, null, 1L, "Charlie", false, new DateTime(2019, 7, 22, 0, 0, 0, 0, DateTimeKind.Utc), false, "Davis", "HR Assistant", 1L },
                    { 6L, null, 2L, "Diana", true, new DateTime(2016, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, "Miller", "Software Developer", 2L },
                    { 7L, null, 2L, "Ethan", true, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Wilson", "Network Administrator", 2L },
                    { 8L, null, 2L, "Fiona", false, new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), false, "Moore", "IT Support", 2L },
                    { 9L, null, 1L, "George", true, new DateTime(2018, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), false, "Taylor", "HR Coordinator", 1L },
                    { 10L, null, 2L, "Hannah", true, new DateTime(2014, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), false, "Anderson", "IT Manager", 2L },
                    { 11L, null, 2L, "Ian", true, new DateTime(2013, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), false, "Thomas", "Database Administrator", 2L },
                    { 12L, null, 1L, "Jack", false, new DateTime(2022, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc), false, "White", "HR Intern", 1L }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "About", "CreatedAt", "DepartmentId", "FirstName", "FullTime", "HireDate", "IsDeleted", "LastName", "Position", "SupervisorId" },
                values: new object[] { 13L, null, new DateTime(2024, 6, 20, 11, 6, 44, 137, DateTimeKind.Utc).AddTicks(8946), 2L, "Karen", true, new DateTime(2019, 10, 30, 0, 0, 0, 0, DateTimeKind.Utc), false, "Hall", "System Analyst", 2L });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DropColumn(
                name: "About",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FullTime",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Position", "SupervisorId" },
                values: new object[] { "Developer", 1L });
        }
    }
}
