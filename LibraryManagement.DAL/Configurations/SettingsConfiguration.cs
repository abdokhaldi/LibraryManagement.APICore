using LibraryManagement.DAL.Configurations.Seed_Data_Constants;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LibraryManagement.DAL.Configurations
{
    public class SettingsConfiguration : IEntityTypeConfiguration<GlobalSettings> 
    {
        public void Configure(EntityTypeBuilder<GlobalSettings> builder)
        {
            builder.ToTable("GlobalSettings");

            builder.HasKey(s => s.SettingsID);

            builder.Property(s => s.DefaultFinePerDay)
                  .IsRequired()
                  .HasColumnType("decimal(18.2)")
                  .HasDefaultValue(10.0m);

            builder.Property(s => s.MaxFineLimit)
                  .IsRequired()
                  .HasColumnType("decimal(18.2)")
                  .HasDefaultValue(100.0m);

            builder.Property(s => s.DefaultBorrowingDays)
                  .IsRequired()
                  .HasDefaultValue(14);

            builder.Property(s => s.MaxBooksPerMember)
                  .IsRequired()
                  .HasDefaultValue(5);

            builder.Property(s => s.IsLibraryOpen);

            builder.Property(s => s.LastUpdated)
                  .IsRequired()
                  .HasDefaultValueSql("GETUTCDATE()");

             builder.Property(s => s.UpdatedBy)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.HasOne<Tenant>()
                .WithOne(t => t.GlobalSettings)
                .HasForeignKey<GlobalSettings>(g=>g.TenantID)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasData(
                new GlobalSettings
               {
                SettingsID = 1,
                TenantID = SeedDataConstants.CasaTenantId,
                DefaultFinePerDay = 10.0m,
                MaxFineLimit = 100.0m,
                DefaultBorrowingDays = 14,
                MaxBooksPerMember = 5,
                IsLibraryOpen = true,
                LastUpdated = DateTime.UtcNow,
                UpdatedBy = "System"
            },
                new GlobalSettings
                {
                    SettingsID = 2,
                    TenantID = SeedDataConstants.RabatTenantId,
                    DefaultFinePerDay = 15.0m,
                    MaxFineLimit = 200.0m,
                    DefaultBorrowingDays = 14,
                    MaxBooksPerMember = 10,
                    IsLibraryOpen = true,
                    LastUpdated = DateTime.UtcNow,
                    UpdatedBy = "System"
                }

                );
        }
    }
}
