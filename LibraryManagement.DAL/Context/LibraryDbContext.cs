using LibraryManagement.DAL.Configurations.Seed_Data_Constants;
using  LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Tenants;
using LibraryManagement.Domain.TenantContract;
using LibraryManagement.Shared.Tenant.TenantContract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace LibraryManagement.DAL.Context
{
    public class LibraryDbContext : DbContext
    {
        private readonly ITenantGetter _tenantGetter;
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, ITenantGetter tenant)
                               : base(options)
        {
            _tenantGetter = tenant;
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
        public DbSet<Tenant> Tenants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);

            
            var currentTenantId = _tenantGetter.GetTenantId();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
               
                if (typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(CreateTenantFilterExpression(entityType.ClrType, currentTenantId));
                }
            }
        }

        private LambdaExpression CreateTenantFilterExpression(Type type , Guid tenantId)
        {
            var parameter = Expression.Parameter(type , "x");
            var property = Expression.Property(parameter, nameof(IMustHaveTenant.TenantID));
            var condition = Expression.Equal(property, Expression.Constant(tenantId));

            return Expression.Lambda(condition, parameter);
        }


       
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentTenantId = _tenantGetter.GetTenantId();

            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.TenantID = currentTenantId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  {
        //     // optionsBuilder.AddInterceptors();
        //  }

    }
}
