using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LibraryManagement.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.BookID);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(b => b.Publisher)
                 .IsRequired()
                 .HasMaxLength(100)
                 .IsUnicode();

            builder.Property(b => b.YearPublished)
                .IsRequired(false)
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.Property(b => b.Quantity)
                .IsRequired();

            builder.Property(b => b.ImagePath)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);


            builder.Property(b => b.IsActive)
                .IsRequired();


            builder.HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b=>b.CategoryID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasData(
                new Book
                {
                    BookID = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Publisher = "Scribner",
                    YearPublished = 1925,
                    Quantity = 10,
                    ImagePath = "images/gatsby.jpg",
                    IsActive = true,
                    CategoryID = 1 // e.g., Fiction
                },
new Book
{
    BookID = 2,
    Title = "A Brief History of Time",
    Author = "Stephen Hawking",
    Publisher = "Bantam Books",
    YearPublished = 1988,
    Quantity = 5,
    ImagePath = "images/hawking_brief.jpg",
    IsActive = true,
    CategoryID = 2 // e.g., Science
},
new Book
{
    BookID = 3,
    Title = "1984",
    Author = "George Orwell",
    Publisher = "Secker & Warburg",
    YearPublished = 1949,
    Quantity = 15,
    ImagePath = "images/1984.jpg",
    IsActive = true,
    CategoryID = 1
},
new Book
{
    BookID = 4,
    Title = "Clean Code",
    Author = "Robert C. Martin",
    Publisher = "Prentice Hall",
    YearPublished = 2008,
    Quantity = 8,
    ImagePath = "images/clean-code.jpg",
    IsActive = true,
    CategoryID = 3 // e.g., Programming
},
new Book
{
    BookID = 5,
    Title = "The Hobbit",
    Author = "J.R.R. Tolkien",
    Publisher = "George Allen & Unwin",
    YearPublished = 1937,
    Quantity = 12,
    ImagePath = "images/hobbit.jpg",
    IsActive = true,
    CategoryID = 1
}
                );

        }
    }
}
