using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


    

namespace LibraryManagement.DAL.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");
            builder.HasKey(m=>m.MemberID);


            builder.Property(m => m.JoinDate)
                .IsRequired();

            builder.Property(m => m.IsActive)
                .IsRequired();


            builder.HasOne(m => m.Person)
                .WithOne()
                .HasForeignKey<Member>(m => m.PersonID)
                .IsRequired();

        }
    }
}
