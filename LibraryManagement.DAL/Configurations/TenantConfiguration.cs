using LibraryManagement.DAL.Configurations.Seed_Data_Constants;
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

        builder.HasData(
            new Tenant
            {
                TenantID = SeedDataConstants.CasaTenantId,
                Name = "مكتبة الدار البيضاء المركزية",
                Identifier = "casablanca-main",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1),
                DefaultLanguage = "ar",
                TimeZone = "W. Central Africa Standard Time" 
            },
        new Tenant
        {
            TenantID = SeedDataConstants.RabatTenantId,
            Name = "Rabat International Library",
            Identifier = "rabat-digital",
            IsActive = true,
            CreatedAt = new DateTime(2026, 1, 1),
            DefaultLanguage = "en",
            TimeZone = "UTC"
        }
            );
        
    }
}