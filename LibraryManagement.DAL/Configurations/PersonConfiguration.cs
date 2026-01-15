using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace LibraryManagement.DAL.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(p=>p.PersonID);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

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
        }
    }
}
