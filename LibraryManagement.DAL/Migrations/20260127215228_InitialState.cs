using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YearPublished = table.Column<short>(type: "smallint", unicode: false, maxLength: 5, nullable: true),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Members_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Borrowings",
                columns: table => new
                {
                    BorrowingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    BorrowingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowings", x => x.BorrowingID);
                    table.ForeignKey(
                        name: "FK_Borrowings_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrowings_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK_Activities_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Programming", "Software development books" },
                    { 2, "History", "World history and biographies" },
                    { 3, "Fiction", "Novels and stories" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonID", "Address", "City", "Email", "FirstName", "Gender", "IsActive", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "Agdal, Rabat", "Rabat", "ahmed.alami@gmail.com", "Ahmed", "M", true, "Alami", "0612345678" },
                    { 2, "Gueliz, Marrakech", "Marrakech", "fatima.ezzahra@outlook.com", "Fatima", "F", true, "Zahra", "0623456789" },
                    { 3, "Maarif, Casablanca", "Casablanca", "youssef.idrissi@yahoo.com", "Youssef", "M", true, "Idrissi", "0634567890" },
                    { 4, "Ville Nouvelle, Fes", "Fes", "sanaa.bennani@gmail.com", "Sanaa", "F", true, "Bennani", "0645678901" },
                    { 5, "Malabata, Tanger", "Tanger", "omar.mansouri@hotmail.com", "Omar", "M", true, "Mansouri", "0656789012" },
                    { 6, "Hay Salam, Agadir", "Agadir", "laila.tazi@gmail.com", "Laila", "F", true, "Tazi", "0667890123" },
                    { 7, "Ouled Ayad, Beni Mellal", "Beni Mellal", "karim.sabbahi@icloud.com", "Karim", "M", true, "Sabbahi", "0678901234" },
                    { 8, "Nansria, Oujda", "Oujda", "meryem.fassi@gmail.com", "Meryem", "F", true, "Fassi", "0689012345" },
                    { 9, "Dakhla, Meknes", "Meknes", "hamza.radi@live.com", "Hamza", "M", true, "Radi", "0690123456" },
                    { 10, "Mohammedia Center", "Mohammedia", "salma.amrani@gmail.com", "Salma", "F", true, "Amrani", "0601234567" },
                    { 11, "Ait Alla , Tabia , Azilal", "Azilal", "Freeh11@gmail.com", "Abdenabi", "M", true, "Khaldi", "0644353219" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Librarian" },
                    { 3, "Member" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "CategoryID", "ImagePath", "IsActive", "Publisher", "Quantity", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", 1, "images/gatsby.jpg", true, "Scribner", (short)10, "The Great Gatsby", (short)1925 },
                    { 2, "Stephen Hawking", 2, "images/hawking_brief.jpg", true, "Bantam Books", (short)5, "A Brief History of Time", (short)1988 },
                    { 3, "George Orwell", 1, "images/1984.jpg", true, "Secker & Warburg", (short)15, "1984", (short)1949 },
                    { 4, "Robert C. Martin", 3, "images/clean-code.jpg", true, "Prentice Hall", (short)8, "Clean Code", (short)2008 },
                    { 5, "J.R.R. Tolkien", 1, "images/hobbit.jpg", true, "George Allen & Unwin", (short)12, "The Hobbit", (short)1937 }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberID", "IsActive", "JoinDate", "PersonID" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7829), 2 },
                    { 2, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7832), 1 },
                    { 3, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7833), 4 },
                    { 4, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7834), 3 },
                    { 5, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7836), 6 },
                    { 6, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7837), 5 },
                    { 7, true, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(7838), 7 }
                });

            migrationBuilder.InsertData(
                table: "Borrowings",
                columns: new[] { "BorrowingID", "BookID", "BorrowingDate", "DueDate", "IsCanceled", "MemberID", "ReturnDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(148), new DateTime(2026, 1, 31, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(151), false, 1, null, "Borrowed" },
                    { 2, 3, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(159), new DateTime(2026, 2, 1, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(159), false, 2, null, "Borrowed" },
                    { 3, 5, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(161), new DateTime(2026, 1, 29, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(162), false, 3, null, "Borrowed" },
                    { 4, 4, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(164), new DateTime(2026, 1, 31, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(164), false, 3, null, "Borrowed" },
                    { 5, 5, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(166), new DateTime(2026, 1, 31, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(167), false, 7, null, "Borrowed" },
                    { 6, 2, new DateTime(2026, 1, 27, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(168), new DateTime(2026, 2, 2, 21, 52, 28, 305, DateTimeKind.Utc).AddTicks(169), false, 7, null, "Borrowed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserID_CreatedAt",
                table: "Activities",
                columns: new[] { "UserID", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_BookID",
                table: "Borrowings",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_MemberID",
                table: "Borrowings",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_PersonID",
                table: "Members",
                column: "PersonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Email",
                table: "People",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Phone",
                table: "People",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_Token",
                table: "UserRefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserID",
                table: "UserRefreshTokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonID",
                table: "Users",
                column: "PersonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Borrowings");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
