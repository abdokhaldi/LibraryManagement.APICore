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


            builder.Property(b => b.ImagePath)
                .IsRequired()
                .HasMaxLength(300)
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
        ImagePath = "7766677788.jpg",
        Description = "A classic novel set in the Roaring Twenties, exploring themes of wealth, love, and the American Dream through the mysterious Jay Gatsby.",
        IsActive = true,
        CategoryID = 1
    },
    new Book
    {
        BookID = 2,
        Title = "A Brief History of Time",
        Author = "Stephen Hawking",
        Publisher = "Bantam Books",
        YearPublished = 1988,
        ImagePath = "7778899900008.jpg",
        Description = "A landmark in scientific writing by one of the world's great minds, explaining the complex concepts of cosmology—from the Big Bang to black holes—in simple terms.",
        IsActive = true,
        CategoryID = 2
    },
    new Book
    {
        BookID = 3,
        Title = "1984",
        Author = "George Orwell",
        Publisher = "Secker & Warburg",
        YearPublished = 1949,
        ImagePath = "54456677.jpg",
        Description = "A chilling dystopian masterpiece that explores the dangers of totalitarianism, surveillance, and the manipulation of truth in a society ruled by Big Brother.",
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
        ImagePath = "67778887776.jpg",
        Description = "An essential guide for software developers, focusing on best practices, principles, and patterns to write code that is readable, maintainable, and professional.",
        IsActive = true,
        CategoryID = 3
    },
    new Book
    {
        BookID = 5,
        Title = "The Hobbit",
        Author = "J.R.R. Tolkien",
        Publisher = "George Allen & Unwin",
        YearPublished = 1937,
        ImagePath = "7776666778.jpg",
        Description = "The unforgettable journey of Bilbo Baggins as he travels through Middle-earth to reclaim a treasure guarded by the dragon Smaug. A prelude to The Lord of the Rings.",
        IsActive = true,
        CategoryID = 1
    }
);

        }
    }
}
