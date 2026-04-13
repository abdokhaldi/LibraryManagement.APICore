using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace LibraryManagement.DAL.Configurations
{
    public class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.ToTable("Fines");

            builder.HasKey(f => f.FineID);

            builder.HasOne(f => f.Member)
                .WithMany(m => m.Fines)
                .HasForeignKey(f => f.MemberID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Borrowing)
                .WithOne(b => b.Fine)
                .HasForeignKey<Fine>(f => f.BorrowingID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();


            builder.Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(f => f.PaidAt)
                .IsRequired(false);
            builder.Property(f => f.WaiveReason)
                .IsRequired(false)
                .HasMaxLength(200)
                .IsUnicode();
    

        }
    }
   
}