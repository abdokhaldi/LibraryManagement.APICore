using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.DAL.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");
            builder.HasKey(a=>a.ActivityID);

            builder.Property(a => a.ActivityType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.EntityID)
                .IsRequired();

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a=>a.UserID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasIndex(a => new {a.UserID, a.CreatedAt });
                

            

                
        }
    }
}
