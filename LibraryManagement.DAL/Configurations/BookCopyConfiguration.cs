using LibraryManagement.Domain.Entities;
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
                    .HasMaxLength(100);

            builder.HasIndex(c => c.Barcode)
                    .IsUnique();

            

            builder.Property<DateTime>("CreatedAt")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Status)
                .HasConversion<string>();

            builder.Property(c => c.Condition)
                .HasMaxLength(500)
                .IsRequired(false);
            
            builder.Property(c => c.IsActive)
                 .HasDefaultValue(true)
                 .IsRequired();

            builder.HasOne(c => c.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(c => c.IsActive);


            // data seeding

            builder.HasData(
   
                new BookCopy { BookCopyID = 1, Barcode = "BC-101-01", Status = CopyStatus.Available, Condition = "New", BookId = 1, IsActive = true },
                new BookCopy { BookCopyID = 2, Barcode = "BC-101-02", Status = CopyStatus.Borrowed, Condition = "Good", BookId = 1, IsActive = true },
                new BookCopy { BookCopyID = 3, Barcode = "BC-101-03", Status = CopyStatus.Available, Condition = "Good", BookId = 1, IsActive = true },
                new BookCopy { BookCopyID = 4, Barcode = "BC-101-04", Status = CopyStatus.Damaged, Condition = "Torn Pages", BookId = 1, IsActive = true },
                new BookCopy { BookCopyID = 5, Barcode = "BC-101-05", Status = CopyStatus.Available, Condition = "New", BookId = 1, IsActive = true },
              
                
                new BookCopy { BookCopyID = 6, Barcode = "BC-202-01", Status = CopyStatus.Available, Condition = "New", BookId = 2, IsActive = true },
                new BookCopy { BookCopyID = 7, Barcode = "BC-202-02", Status = CopyStatus.Borrowed, Condition = "Good", BookId = 2, IsActive = true },
                new BookCopy { BookCopyID = 8, Barcode = "BC-202-03", Status = CopyStatus.Borrowed, Condition = "Excellent", BookId = 2, IsActive = true },
                new BookCopy { BookCopyID = 9, Barcode = "BC-202-04", Status = CopyStatus.Lost, Condition = "Missing", BookId = 2, IsActive = true },
                new BookCopy { BookCopyID = 10, Barcode = "BC-202-05", Status = CopyStatus.Available, Condition = "Good", BookId = 2, IsActive = true },

   
                new BookCopy { BookCopyID = 11, Barcode = "BC-303-01", Status = CopyStatus.Available, Condition = "New", BookId = 3, IsActive = true },
                new BookCopy { BookCopyID = 12, Barcode = "BC-303-02", Status = CopyStatus.Reserved, Condition = "Good", BookId = 3, IsActive = true },
                new BookCopy { BookCopyID = 13, Barcode = "BC-303-03", Status = CopyStatus.Available, Condition = "New", BookId = 3, IsActive = true },
                new BookCopy { BookCopyID = 14, Barcode = "BC-303-04", Status = CopyStatus.Available, Condition = "Fair", BookId = 3, IsActive = true },
                new BookCopy { BookCopyID = 15, Barcode = "BC-303-05", Status = CopyStatus.Damaged, Condition = "Water Damage", BookId = 3, IsActive = true },
              
               
                new BookCopy { BookCopyID = 16, Barcode = "BC-404-01", Status = CopyStatus.Available, Condition = "New", BookId = 4, IsActive = true },
                new BookCopy { BookCopyID = 17, Barcode = "BC-404-02", Status = CopyStatus.Borrowed, Condition = "Good", BookId = 4, IsActive = true },
                new BookCopy { BookCopyID = 18, Barcode = "BC-404-03", Status = CopyStatus.Available, Condition = "Good", BookId = 4, IsActive = true },
                new BookCopy { BookCopyID = 19, Barcode = "BC-404-04", Status = CopyStatus.Available, Condition = "New", BookId = 4, IsActive = true },
                new BookCopy { BookCopyID = 20, Barcode = "BC-404-05", Status = CopyStatus.Available, Condition = "New", BookId = 4, IsActive = true }
            );
        }

        
    }
}
