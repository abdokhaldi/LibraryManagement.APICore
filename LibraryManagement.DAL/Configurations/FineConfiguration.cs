using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace LibraryManagement.DAL.Configurations
{
    public class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.ToTable("Fines");
            builder.HasKey(f => f.FineID);
            builder.HasOne(f => f.Member)
                .WithMany(m => m.Fines)
                .HasForeignKey(f=> f.)
        }
    }
   
}