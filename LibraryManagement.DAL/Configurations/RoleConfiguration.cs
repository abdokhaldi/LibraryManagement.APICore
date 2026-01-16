using Microsoft.EntityFrameworkCore;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r=>r.RoleID);

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Role {RoleID = 1, RoleName = "Admin" },
                new Role {RoleID = 2 , RoleName = "Librarian" },
                new Role {RoleID = 3 , RoleName = "Member" }
                );
        }
    }
}
