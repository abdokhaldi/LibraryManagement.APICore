using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using System.Net;

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

            builder.HasData(
new Borrowing()
{
 BorrowingID = 1,
 BookID = 1,
 MemberID = 1,
 BorrowingDate = DateTime.UtcNow,
 DueDate = DateTime.UtcNow.AddDays(4),
 ReturnDate = null,
 Status = "Borrowed",
 IsCanceled = false,

},
new Borrowing()
{
  BorrowingID = 2,
  BookID = 3,
  MemberID = 2,
  BorrowingDate = DateTime.UtcNow,
  DueDate = DateTime.UtcNow.AddDays(5),
  ReturnDate = null,
  Status = "Borrowed",
  IsCanceled = false,

},
new Borrowing()
{
   BorrowingID = 3,
   BookID = 5,
   MemberID = 3,
   BorrowingDate = DateTime.UtcNow,
   DueDate = DateTime.UtcNow.AddDays(2),
   ReturnDate = null,
   Status = "Borrowed",
   IsCanceled = false,

},
new Borrowing()
{
    BorrowingID = 4,
    BookID = 4,
    MemberID = 3,
    BorrowingDate = DateTime.UtcNow,
    DueDate = DateTime.UtcNow.AddDays(4),
    ReturnDate = null,
    Status = "Borrowed",
    IsCanceled = false,

},
 new Borrowing()
 {
     BorrowingID = 5,
     BookID = 5,
     MemberID = 7,
     BorrowingDate = DateTime.UtcNow,
     DueDate = DateTime.UtcNow.AddDays(4),
     ReturnDate = null,
     Status = "Borrowed",
     IsCanceled = false,

 },
  new Borrowing()
  {
      BorrowingID = 6,
      BookID = 2,
      MemberID = 7,
      BorrowingDate = DateTime.UtcNow,
      DueDate = DateTime.UtcNow.AddDays(6),
      ReturnDate = null,
      Status = "Borrowed",
      IsCanceled = false,

  }
);


        }
    }
}
