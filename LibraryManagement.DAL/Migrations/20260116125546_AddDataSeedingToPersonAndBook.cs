using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSeedingToPersonAndBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "PersonID",
                keyValue: 11);
        }
    }
}
