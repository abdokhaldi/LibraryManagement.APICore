using Microsoft.EntityFrameworkCore;
using  LibraryManagement.Domain.Entities;
namespace LibraryManagement.DAL.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
                               : base(options)
        {

        }

       
        public DbSet<Person> People { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<GlobalSettings> GlobalSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
                
        }

    }
}
