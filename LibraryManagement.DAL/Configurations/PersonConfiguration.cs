using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LibraryManagement.DAL.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(p=>p.PersonID);

            builder.Property(p => p.PersonID)
                .ValueGeneratedNever();

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.HasIndex(p => p.NationalNumber)
                .IsUnique();

            builder.Property(p => p.NationalNumber)
                .IsRequired()
                .HasMaxLength(20);
                
            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
                

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);


            builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode();

            builder.Property(p => p.IsActive)
                .IsRequired();

            // indexes
            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.HasIndex(p => p.Phone)
                .IsUnique();

            builder.HasOne<Tenant>()
                .WithMany()
                .HasForeignKey(p => p.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

           
        }

        
    }
}
