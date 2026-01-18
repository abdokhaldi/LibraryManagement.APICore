using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeedingToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "IsActive", "IsBlocked", "Password", "PersonID", "RoleID", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 16, 13, 4, 29, 976, DateTimeKind.Utc).AddTicks(102), true, false, "123456", 11, 1, "abdokhal12" },
                    { 2, new DateTime(2026, 1, 16, 13, 4, 29, 976, DateTimeKind.Utc).AddTicks(105), true, false, "123456", 2, 2, "fatima12" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);
        }
    }
}
