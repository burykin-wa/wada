using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class init_employees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "IsDeleted", "LastName", "Position", "SupervisorId" },
                values: new object[,]
                {
                    { 1L, "John", false, "Doe", "Manager", null },
                    { 2L, "Jane", false, "Smith", "Developer", 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
