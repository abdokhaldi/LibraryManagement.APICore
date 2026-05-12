using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LibraryManagement.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u=>u.UserID);

            builder.Property(u => u.UserID)
                .ValueGeneratedNever();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .IsUnicode(false);
                

            builder.HasOne(u => u.Person)
                .WithOne()
                .HasForeignKey<User>(u => u.PersonID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


            // indexes

            builder.HasIndex(u=>u.Username)
                .IsUnique();

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(u => u.TenantID)
                .OnDelete(DeleteBehavior.Restrict);
           
        }

        
    }
}
