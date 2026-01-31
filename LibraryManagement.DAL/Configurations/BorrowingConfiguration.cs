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

            builder.HasOne(b => b.BookCopy)
                 .WithMany(b => b.Borrowings)
                 .HasForeignKey(b => b.BookCopyID)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            builder.HasData(
    new Borrowing()
    {
        BorrowingID = 1,
        BookCopyID = 2, // نسخة كتاب 1 (كانت Borrowed في Seed النسخ)
        MemberID = 1,
        BorrowingDate = DateTime.UtcNow,
        DueDate = DateTime.UtcNow.AddDays(4),
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
        ReturnDate = null,
        Status = "Borrowed",
        IsCanceled = false
    },
    new Borrowing()
    {
        BorrowingID = 3,
        BookCopyID = 8, // نسخة كتاب 2 (كانت Borrowed في Seed النسخ)
        MemberID = 3,
        BorrowingDate = DateTime.UtcNow,
        DueDate = DateTime.UtcNow.AddDays(2),
        ReturnDate = null,
        Status = "Borrowed",
        IsCanceled = false
    },
    new Borrowing()
    {
        BorrowingID = 4,
        BookCopyID = 17, // نسخة كتاب 4 (كانت Borrowed في Seed النسخ)
        MemberID = 3,
        BorrowingDate = DateTime.UtcNow,
        DueDate = DateTime.UtcNow.AddDays(4),
        ReturnDate = null,
        Status = "Borrowed",
        IsCanceled = false
    },
    new Borrowing()
    {
        BorrowingID = 5,
        BookCopyID = 2, // استعارة تاريخية منتهية لنفس النسخة رقم 2 (مثال)
        MemberID = 7,
        BorrowingDate = DateTime.UtcNow.AddMonths(-1),
        DueDate = DateTime.UtcNow.AddMonths(-1).AddDays(7),
        ReturnDate = DateTime.UtcNow.AddMonths(-1).AddDays(5),
        Status = "Returned", // حالة مكتملة
        IsCanceled = false
    },
    new Borrowing()
    {
        BorrowingID = 6,
        BookCopyID = 12, 
        MemberID = 7,
        BorrowingDate = DateTime.UtcNow,
        DueDate = DateTime.UtcNow.AddDays(6),
        ReturnDate = null,
        Status = "Borrowed",
        IsCanceled = false
    }
);


        }
    }
}
