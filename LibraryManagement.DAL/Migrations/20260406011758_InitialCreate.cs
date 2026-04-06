using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    ISBN = table.Column<string>(type: "char(13)", unicode: false, fixedLength: true, maxLength: 13, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    YearPublished = table.Column<short>(type: "smallint", unicode: false, maxLength: 5, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false)
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
                name: "BookCopies",
                columns: table => new
                {
                    BookCopyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopies", x => x.BookCopyID);
                    table.ForeignKey(
                        name: "FK_BookCopies_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
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

            migrationBuilder.CreateTable(
                name: "Borrowings",
                columns: table => new
                {
                    BorrowingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookCopyID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Borrowings_BookCopies_BookCopyID",
                        column: x => x.BookCopyID,
                        principalTable: "BookCopies",
                        principalColumn: "BookCopyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrowings_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
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
                columns: new[] { "BookID", "Author", "CategoryID", "CreatedAt", "Description", "ISBN", "ImagePath", "IsActive", "Publisher", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A classic novel set in the Roaring Twenties, exploring themes of wealth, love, and the American Dream through the mysterious Jay Gatsby.", "9780743273565", "7766677788.jpg", true, "Scribner", "The Great Gatsby", (short)1925 },
                    { 2, "Stephen Hawking", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A landmark in scientific writing by one of the world's great minds, explaining the complex concepts of cosmology—from the Big Bang to black holes—in simple terms.", "9780553380163", "7778899900008.jpg", true, "Bantam Books", "A Brief History of Time", (short)1988 },
                    { 3, "George Orwell", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A chilling dystopian masterpiece that explores the dangers of totalitarianism, surveillance, and the manipulation of truth in a society ruled by Big Brother.", "9780451524935", "54456677.jpg", true, "Secker & Warburg", "1984", (short)1949 },
                    { 4, "Robert C. Martin", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An essential guide for software developers, focusing on best practices, principles, and patterns to write code that is readable, maintainable, and professional.", "9780132350884", "67778887776.jpg", true, "Prentice Hall", "Clean Code", (short)2008 },
                    { 5, "J.R.R. Tolkien", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The unforgettable journey of Bilbo Baggins as he travels through Middle-earth to reclaim a treasure guarded by the dragon Smaug. A prelude to The Lord of the Rings.", "9780547928227", "7776666778.jpg", true, "George Allen & Unwin", "The Hobbit", (short)1937 }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberID", "IsActive", "JoinDate", "PersonID" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4718), 2 },
                    { 2, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4723), 1 },
                    { 3, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4724), 4 },
                    { 4, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4726), 3 },
                    { 5, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4728), 6 },
                    { 6, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4729), 5 },
                    { 7, true, new DateTime(2026, 4, 6, 1, 17, 57, 389, DateTimeKind.Utc).AddTicks(4731), 7 }
                });

            migrationBuilder.InsertData(
                table: "BookCopies",
                columns: new[] { "BookCopyID", "Barcode", "BookID", "Condition", "IsActive", "Status" },
                values: new object[,]
                {
                    { 1, "BC-101-01", 1, "New", true, "Available" },
                    { 2, "BC-101-02", 1, "Good", true, "Borrowed" },
                    { 3, "BC-101-03", 1, "Good", true, "Available" },
                    { 4, "BC-101-04", 1, "Torn Pages", true, "Damaged" },
                    { 5, "BC-101-05", 1, "New", true, "Available" },
                    { 6, "BC-202-01", 2, "New", true, "Available" },
                    { 7, "BC-202-02", 2, "Good", true, "Borrowed" },
                    { 8, "BC-202-03", 2, "Excellent", true, "Borrowed" },
                    { 9, "BC-202-04", 2, "Missing", true, "Lost" },
                    { 10, "BC-202-05", 2, "Good", true, "Available" },
                    { 11, "BC-303-01", 3, "New", true, "Available" },
                    { 12, "BC-303-02", 3, "Good", true, "Reserved" },
                    { 13, "BC-303-03", 3, "New", true, "Available" },
                    { 14, "BC-303-04", 3, "Fair", true, "Available" },
                    { 15, "BC-303-05", 3, "Water Damage", true, "Damaged" },
                    { 16, "BC-404-01", 4, "New", true, "Available" },
                    { 17, "BC-404-02", 4, "Good", true, "Borrowed" },
                    { 18, "BC-404-03", 4, "Good", true, "Available" },
                    { 19, "BC-404-04", 4, "New", true, "Available" },
                    { 20, "BC-404-05", 4, "New", true, "Available" }
                });

            migrationBuilder.InsertData(
                table: "Borrowings",
                columns: new[] { "BorrowingID", "BookCopyID", "BorrowingDate", "DueDate", "IsCanceled", "MemberID", "ReturnDate", "Status" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4364), new DateTime(2026, 4, 10, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4366), false, 1, null, "Borrowed" },
                    { 2, 7, new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4373), new DateTime(2026, 4, 11, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4373), false, 2, null, "Borrowed" },
                    { 3, 8, new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4376), new DateTime(2026, 4, 8, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4376), false, 3, null, "Borrowed" },
                    { 4, 17, new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4378), new DateTime(2026, 4, 10, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4379), false, 3, null, "Borrowed" },
                    { 5, 2, new DateTime(2026, 3, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4381), new DateTime(2026, 3, 13, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4388), false, 7, new DateTime(2026, 3, 11, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4389), "Returned" },
                    { 6, 12, new DateTime(2026, 4, 6, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4398), new DateTime(2026, 4, 12, 1, 17, 57, 388, DateTimeKind.Utc).AddTicks(4399), false, 7, null, "Borrowed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserID_CreatedAt",
                table: "Activities",
                columns: new[] { "UserID", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_Barcode",
                table: "BookCopies",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopies_BookID",
                table: "BookCopies",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_BookCopyID",
                table: "Borrowings",
                column: "BookCopyID");

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
                name: "BookCopies");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
