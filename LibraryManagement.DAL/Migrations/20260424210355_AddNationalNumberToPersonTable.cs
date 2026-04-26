using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalNumberToPersonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "People",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 1,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 24, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6763), new DateTime(2026, 4, 28, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6765) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 2,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 24, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6775), new DateTime(2026, 4, 29, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6775) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 3,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 24, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6778), new DateTime(2026, 4, 26, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6778) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 4,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 24, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6781), new DateTime(2026, 4, 28, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6781) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 5,
                columns: new[] { "BorrowingDate", "DueDate", "ReturnDate" },
                values: new object[] { new DateTime(2026, 3, 24, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6784), new DateTime(2026, 3, 31, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6790), new DateTime(2026, 3, 29, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6791) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "BorrowingID",
                keyValue: 6,
                columns: new[] { "BorrowingDate", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 24, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6796), new DateTime(2026, 4, 30, 21, 3, 54, 248, DateTimeKind.Utc).AddTicks(6797) });

            migrationBuilder.UpdateData(
                table: "GlobalSettings",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 252, DateTimeKind.Utc).AddTicks(7396));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 1,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4409));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 2,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 3,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4415));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 4,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4416));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 5,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4418));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 6,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4419));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberID",
                keyValue: 7,
                column: "JoinDate",
                value: new DateTime(2026, 4, 24, 21, 3, 54, 250, DateTimeKind.Utc).AddTicks(4421));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 1,
                column: "NationalNumber",
                value: "IC122065");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 2,
                column: "NationalNumber",
                value: "IC122068");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 3,
                column: "NationalNumber",
                value: "IC922065");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 4,
                column: "NationalNumber",
                value: "IC922965");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 5,
                column: "NationalNumber",
                value: "IC128493");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 6,
                column: "NationalNumber",
                value: "I907065");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 7,
                column: "NationalNumber",
                value: "IC87409");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 8,
                column: "NationalNumber",
                value: "IC008571");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 9,
                column: "NationalNumber",
                value: "IC124598");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 10,
                column: "NationalNumber",
                value: "IC248382");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 11,
                column: "NationalNumber",
                value: "IC188555");

            migrationBuilder.CreateIndex(
                name: "IX_People_NationalNumber",
                table: "People",
                column: "NationalNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_NationalNumber",
                table: "People");

            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "People");

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

            migrationBuilder.UpdateData(
                table: "GlobalSettings",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2026, 4, 15, 12, 22, 48, 874, DateTimeKind.Utc).AddTicks(1621));

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
    }
}
