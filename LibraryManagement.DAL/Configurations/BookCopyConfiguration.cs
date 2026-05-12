using LibraryManagement.DAL.Configurations.Seed_Data_Constants;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.Shared.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.DAL.Configurations
{
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {

        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.HasKey(c => c.BookCopyID);
            builder.ToTable("BookCopies");
            
            builder.Property(c => c.Barcode)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(13);

            builder.HasIndex(c => c.Barcode)
                    .IsUnique();

            

            builder.Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Status)
                .HasConversion<string>();

            builder.Property(c => c.Condition)
                .HasMaxLength(100)
                .IsRequired(false);
            
            builder.Property(c => c.IsActive)
                 .HasDefaultValue(true)
                 .IsRequired();

            builder.HasOne(c => c.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(c => c.BookID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(c => c.IsActive);

            builder.HasOne<Tenant>()
               .WithMany()
               .HasForeignKey(c => c.TenantID)
               .OnDelete(DeleteBehavior.Restrict);

            
           
        }

        
    }
}
