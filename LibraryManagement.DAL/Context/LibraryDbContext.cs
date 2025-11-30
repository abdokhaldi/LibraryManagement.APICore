using System;
using Microsoft.EntityFrameworkCore;
using  LibraryManagement.DAL.Entities;
namespace LibraryManagement.DAL.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        // DbSets for real tables
        public DbSet<Person> People { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }

        // DbSets for views
        public DbSet<BorrowingInfoView> FullBorrowingsInfo { get; set; }
        public DbSet<SmallPersonEntity> SmallPeople { get; set; }

        public DbSet<SmallBookEntity> SmallBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowingInfoView>().HasNoKey().ToView("FullBorrowingsInfo");
            modelBuilder.Entity<SmallBookEntity>().HasNoKey().ToView("SmallBooks");
            modelBuilder.Entity<SmallPersonEntity>().HasNoKey().ToView("SmallPeople");
            base.OnModelCreating(modelBuilder);
        }

    }
}
