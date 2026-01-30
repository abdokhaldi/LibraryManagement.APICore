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

            builder.HasData(
                new Member()
                {
                    MemberID = 1,
                    PersonID = 2,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 2,
                    PersonID = 1,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 3,
                    PersonID = 4,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 4,
                    PersonID = 3,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 5,
                    PersonID = 6,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 6,
                    PersonID = 5,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                 new Member()
                 {
                     MemberID = 7,
                     PersonID = 7,
                     JoinDate = DateTime.UtcNow,
                     IsActive = true
                 }
                );
        }
    }
}
