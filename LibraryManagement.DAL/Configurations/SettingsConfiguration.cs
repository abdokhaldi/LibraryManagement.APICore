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
            builder.Property(s => s.SettingsID)
                .ValueGeneratedNever();

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

            builder.Property(s => s.DefaultLanguage)
                .IsRequired()
                .HasDefaultValue("en")
                .HasMaxLength(10);

            builder.Property(s => s.TimeZone)
                .IsRequired()
                .HasDefaultValue("UTC")
                .IsUnicode(false)
                .HasMaxLength(50);

           

             builder.Property(s => s.UpdatedBy)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

            builder.HasOne<Tenant>()
                .WithOne(t => t.GlobalSettings)
                .HasForeignKey<GlobalSettings>(g => g.TenantID)
                .OnDelete(DeleteBehavior.Cascade);


              
        }
    }
}
