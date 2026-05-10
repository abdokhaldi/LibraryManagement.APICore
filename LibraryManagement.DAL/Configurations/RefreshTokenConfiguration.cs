using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.DAL.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            
            builder.ToTable("UserRefreshTokens");

           
            builder.HasKey(rt => rt.ID);

            
            builder.Property(rt => rt.Token)
                   .IsRequired()
                   .HasMaxLength(500); 

           
            builder.HasIndex(rt => rt.Token)
                   .IsUnique();

            
            builder.Property(rt => rt.Expires)
                   .IsRequired();

            builder.Property(rt => rt.Created)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");
            
            builder.HasOne(rt => rt.User)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(rt => rt.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(rt => rt.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}