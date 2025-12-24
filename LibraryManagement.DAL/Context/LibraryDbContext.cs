using System;
using Microsoft.EntityFrameworkCore;
using  LibraryManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore.Internal;
namespace LibraryManagement.DAL.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        // DbSets for tables
        public DbSet<Person> People { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Member> Members { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                  .HasIndex(p => new { p.Email, p.Phone })
                  .IsUnique();
            modelBuilder.Entity<User>()
                 .HasIndex(u => u.Username)
                 .IsUnique();
           
             base.OnModelCreating(modelBuilder);
            //
            
        }

    }
}
