using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
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

            builder.Property(b => b.InitialFees)
                .HasColumnType ("decimal(18.2)")
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

            builder.HasOne(b => b.BookCopy)
                 .WithMany(b => b.Borrowings)
                 .HasForeignKey(b => b.BookCopyID)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            builder.HasQueryFilter(b => b.BookCopy.IsActive);

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(b => b.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
            new Borrowing()
            {
                BorrowingID = 1,
                BookCopyID = 2,
                MemberID = 1,
                BorrowingDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(4),
                InitialFees = 15.0m,
        
                ReturnDate = null,
                Status = "Borrowed",
                IsCanceled = false
            },
            new Borrowing()
            {
                BorrowingID = 2,
                BookCopyID = 7,
                MemberID = 2,
                BorrowingDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(5),
                InitialFees = 15.0m,
                ReturnDate = null,
                Status = "Borrowed",
                IsCanceled = false
            },
            new Borrowing()
            {
                BorrowingID = 3,
                BookCopyID = 8, 
                MemberID = 3,
                BorrowingDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(2),
                InitialFees = 15.0m,
                ReturnDate = null,
                Status = "Borrowed",
                IsCanceled = false
            },
            new Borrowing()
            {
                BorrowingID = 4,
                BookCopyID = 17, 
                MemberID = 3,
                BorrowingDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(4),
                InitialFees = 15.0m,
                ReturnDate = null,
                Status = "Borrowed",
                IsCanceled = false
            },
            new Borrowing()
            {
                BorrowingID = 5,
                BookCopyID = 2, 
                MemberID = 7,
                BorrowingDate = DateTime.UtcNow.AddMonths(-1),
                DueDate = DateTime.UtcNow.AddMonths(-1).AddDays(7),
                InitialFees = 15.0m,
                ReturnDate = DateTime.UtcNow.AddMonths(-1).AddDays(5),
                Status = "Returned", 
                IsCanceled = false
            },
            new Borrowing()
            {
                BorrowingID = 6,
                BookCopyID = 12,
                MemberID = 7,
                BorrowingDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(6),
                InitialFees = 15.0m,
                ReturnDate = null,
                Status = "Borrowed",
                IsCanceled = false
            }
        );


        }
    }
}
