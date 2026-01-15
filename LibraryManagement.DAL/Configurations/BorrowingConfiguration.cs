using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.DAL.Configurations
{
    public class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
    {

        public void Configure(EntityTypeBuilder<Borrowing> builder)
        {
            builder.ToTable("Borrowings");
            builder.HasKey(b=>b.BorrowingID);

            builder.Property(b => b.BorrowingDate)
                .IsRequired();

            builder.Property(b => b.ReturnDate)
                .IsRequired(false);

            builder.Property(b => b.DueDate)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.IsCanceled)
                .IsRequired();

            // Foreign keys 
            builder.HasOne(b => b.Member)
                 .WithMany()
                 .HasForeignKey(b => b.MemberID)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            builder.HasOne(b => b.Book)
                 .WithMany()
                 .HasForeignKey(b => b.BookID)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
                
        }
    }
}
