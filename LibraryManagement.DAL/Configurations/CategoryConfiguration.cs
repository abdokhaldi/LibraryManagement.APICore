using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
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

           
            builder.HasData(
            new Category { CategoryID = 1, CategoryName = "Programming", Description = "Software development books" },
            new Category { CategoryID = 2, CategoryName = "History", Description = "World history and biographies" },
            new Category { CategoryID = 3, CategoryName = "Fiction", Description = "Novels and stories" }
        );

        }
    }
}
