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

            builder.HasKey(s => s.ID);

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
                .WithMany()
                .HasForeignKey(g => g.TenantID)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(new GlobalSettings
            {
                ID = 1,
                DefaultFinePerDay = 10.0m,
                MaxFineLimit = 100.0m,
                DefaultBorrowingDays = 14,
                MaxBooksPerMember = 5,
                IsLibraryOpen = true,
                LastUpdated = DateTime.UtcNow,
                UpdatedBy = "System"
            });
        }
    }
}
