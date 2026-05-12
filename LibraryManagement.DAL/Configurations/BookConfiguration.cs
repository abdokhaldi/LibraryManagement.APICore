using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
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

            builder.HasOne<Tenant>()
              .WithMany()
              .HasForeignKey(b => b.TenantID)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode();

            builder.HasIndex(b => b.Title)
                .IsUnique();

            builder.Property(b => b.ISBN)
               .IsRequired()
               .HasMaxLength(13)
               .IsFixedLength()
               .IsUnicode(false);

            builder.HasIndex(b => b.ISBN)
                .IsUnique();

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(b => b.Publisher)
                 .IsRequired()
                 .HasMaxLength(100)
                 .IsUnicode();

            builder.Property(b => b.YearPublished)
                .IsRequired()
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

           
    
        }
    }
}
