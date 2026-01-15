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

            

        }
    }
}
