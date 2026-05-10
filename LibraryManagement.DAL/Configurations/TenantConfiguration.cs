using LibraryManagement.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        builder.HasKey(t => t.TenantID);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode();

        builder.Property(t => t.Identifier)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);

        
        builder.HasIndex(t => t.Identifier).IsUnique();

        builder.Property(t => t.DefaultLanguage)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasDefaultValue("ar"); 

        builder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(t => t.TimeZone)
            .HasMaxLength(50)
            .HasDefaultValue("UTC");

        builder.Property(t => t.IsActive)
            .HasDefaultValue(true);

        
        builder.HasMany(t => t.Users)
               .WithOne()
               .HasForeignKey(u => u.TenantID)
               .OnDelete(DeleteBehavior.Restrict);
        
    }
}