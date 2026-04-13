using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFinesTbl_UpdateBorrowingsTblToAddInitalFeesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialFees",
                table: "Borrowings",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowingID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaiveReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineID);
                    table.ForeignKey(
                        name: "FK_Fines_Borrowings_BorrowingID",
                        column: x => x.BorrowingID,
                        principalTable: "Borrowings",
                        principalColumn: "BorrowingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fines_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 1,
                columns: new[] { "BorrowingDate", "DueDate", "InitialFees" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6188), new DateTime(2026, 4, 16, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6189), 15.0m });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 2,
                columns: new[] { "BorrowingDate", "DueDate", "InitialFees" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6198), new DateTime(2026, 4, 17, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6198), 15.0m });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 3,
                columns: new[] { "BorrowingDate", "DueDate", "InitialFees" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6202), new DateTime(2026, 4, 14, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6202), 15.0m });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 4,
                columns: new[] { "BorrowingDate", "DueDate", "InitialFees" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6206), new DateTime(2026, 4, 16, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6206), 15.0m });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 5,
                columns: new[] { "BorrowingDate", "DueDate", "InitialFees", "ReturnDate" },
                values: new object[] { new DateTime(2026, 3, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6209), new DateTime(2026, 3, 19, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6216), 15.0m, new DateTime(2026, 3, 17, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6217) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 6,
                columns: new[] { "BorrowingDate", "DueDate", "InitialFees" },
                values: new object[] { new DateTime(2026, 4, 12, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6222), new DateTime(2026, 4, 18, 5, 39, 48, 345, DateTimeKind.Utc).AddTicks(6223), 15.0m });

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

            migrationBuilder.CreateIndex(
                name: "IX_Fines_BorrowingID",
                table: "Fines",
                column: "BorrowingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_MemberID",
                table: "Fines",
                column: "MemberID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropColumn(
                name: "InitialFees",
                table: "Borrowings");

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 1,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4364), new DateTime(2026, 4, 10, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4366) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 2,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4373), new DateTime(2026, 4, 11, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4373) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 3,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4376), new DateTime(2026, 4, 8, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4376) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 4,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4378), new DateTime(2026, 4, 10, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4379) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 5,
                columns: new[] { "BorrowingDate", "DueDate", "ReturnDate" },
                values: new object[] { new DateTime(2026, 3, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4381), new DateTime(2026, 3, 13, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4388), new DateTime(2026, 3, 11, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4389) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 6,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4398), new DateTime(2026, 4, 12, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4399) });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4718));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4723));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4724));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4726));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4728));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 6,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4729));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 7,
                column: "JoinDate",
                value: new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4731));
        }
    }
}
