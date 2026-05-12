using LibraryManagement.DAL.Configurations.Seed_Data_Constants;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
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

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(m => m.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Member()
                {
                    MemberID = 1,

                    PersonID = 2,
                    TenantID = SeedDataConstants.CasaTenantId,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 2,
                    PersonID = 1,
                    TenantID = SeedDataConstants.CasaTenantId,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 3,
                    PersonID = 4,
                    TenantID = SeedDataConstants.CasaTenantId,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 4,
                    PersonID = 3,
                    TenantID = SeedDataConstants.CasaTenantId,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 5,
                    PersonID = 6,
                    TenantID = SeedDataConstants.RabatTenantId,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Member()
                {
                    MemberID = 6,
                    PersonID = 5,
                    TenantID = SeedDataConstants.RabatTenantId,
                    JoinDate = DateTime.UtcNow,
                    IsActive = true
                },
                 new Member()
                 {
                     MemberID = 7,
                     PersonID = 7,
                     TenantID = SeedDataConstants.RabatTenantId,
                     JoinDate = DateTime.UtcNow,
                     IsActive = true
                 }
                );
        }
    }
}
