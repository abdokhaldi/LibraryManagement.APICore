using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGlobalSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultFinePerDay = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 10.0m),
                    MaxFineLimit = table.Column<decimal>(type: "decimal(18,0)", nullable: false, defaultValue: 100.0m),
                    DefaultBorrowingDays = table.Column<int>(type: "int", nullable: false, defaultValue: 14),
                    MaxBooksPerMember = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    IsLibraryOpen = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 1,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 15, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3864), new DateTime(2026, 4, 19, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3866) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 2,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 15, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3873), new DateTime(2026, 4, 20, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3874) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 3,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 15, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3877), new DateTime(2026, 4, 17, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3877) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 4,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 15, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3880), new DateTime(2026, 4, 19, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 5,
                columns: new[] { "BorrowingDate", "DueDate", "ReturnDate" },
                values: new object[] { new DateTime(2026, 3, 15, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3883), new DateTime(2026, 3, 22, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3888), new DateTime(2026, 3, 20, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3890) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 6,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 15, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3895), new DateTime(2026, 4, 21, 12, 22, 48, 870, DateTimeKind.Utc).AddTicks(3895) });

            migrationBuilder.InsertData(
                table: "GlobalSettings",
                columns: new[] { "ID", "DefaultBorrowingDays", "DefaultFinePerDay", "IsLibraryOpen", "LastUpdated", "MaxBooksPerMember", "MaxFineLimit", "UpdatedBy" },
                values: new object[] { 1, 14, 10.0m, true, new DateTime(2026, 4, 15, 12, 22, 48, 874, DateTimeKind.Utc).AddTicks(1621), 5, 100.0m, "System" });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1202));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1203));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1205));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1206));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 6,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1208));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 7,
                column: "JoinDate",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 872, DateTimeKind.Utc).AddTicks(1209));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 1,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6188), new DateTime(2026, 4, 16, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6189) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 2,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6198), new DateTime(2026, 4, 17, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6198) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 3,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6202), new DateTime(2026, 4, 14, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6202) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 4,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6206), new DateTime(2026, 4, 16, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6206) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 5,
                columns: new[] { "BorrowingDate", "DueDate", "ReturnDate" },
                values: new object[] { new DateTime(2026, 3, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6209), new DateTime(2026, 3, 19, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6216), new DateTime(2026, 3, 17, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6217) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 6,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6222), new DateTime(2026, 4, 18, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6223) });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3916));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3922));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3924));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 6,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 7,
                column: "JoinDate",
                value: new DateTime(2026, 4, 12, 5, 39, 48, 347, DateTimeKind.Utc).AddTicks(3927));
        }
    }
}
