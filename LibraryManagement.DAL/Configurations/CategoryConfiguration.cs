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


            builder.HasData(
     // علوم الحاسوب والتقنية
     new Category { CategoryID = 1, CategoryName = "Programming", Description = "Software development, languages, and tools" },
     new Category { CategoryID = 2, CategoryName = "Computer Science", Description = "Theoretical foundations, algorithms, and AI" },
     new Category { CategoryID = 3, CategoryName = "Cybersecurity", Description = "Network security, ethical hacking, and cryptography" },

     // العلوم الإنسانية
     new Category { CategoryID = 4, CategoryName = "History", Description = "World history, civilizations, and biographies" },
     new Category { CategoryID = 5, CategoryName = "Philosophy", Description = "Classical and modern philosophical thoughts" },
     new Category { CategoryID = 6, CategoryName = "Psychology", Description = "Human behavior, mental health, and social psychology" },

     // الأدب والخيال
     new Category { CategoryID = 7, CategoryName = "Fiction", Description = "Novels, short stories, and literary works" },
     new Category { CategoryID = 8, CategoryName = "Science Fiction", Description = "Space exploration, time travel, and futuristic tech" },
     new Category { CategoryID = 9, CategoryName = "Poetry", Description = "Classical and contemporary poetic collections" },

     // العلوم الطبيعية (التي تحبها)
     new Category { CategoryID = 10, CategoryName = "Astronomy & Physics", Description = "Space, cosmos, quantum mechanics, and astrophysics" },
     new Category { CategoryID = 11, CategoryName = "Mathematics", Description = "Pure and applied mathematics, statistics, and logic" },

     // المال والأعمال
     new Category { CategoryID = 12, CategoryName = "Business & Finance", Description = "Economy, management, and personal finance" },
     new Category { CategoryID = 13, CategoryName = "Self-Help", Description = "Personal development, productivity, and leadership" }
 );

        }
    }
}
