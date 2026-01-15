using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c=>c.CategoryID);

            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(300)
                .IsUnicode();
        }
    }
}
