using ExpenseWebAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseWebAppDAL.Configuration;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppDAL.Interceptors;

namespace ExpenseWebAppDAL.Data
{
    public class WebAppContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public WebAppContext(DbContextOptions options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var trackedEntities = ChangeTracker.Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            foreach(var entity in trackedEntities)
            {
                entity.State = EntityState.Modified;
                entity.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
                entity.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = DateTimeOffset.Now.AddHours(1);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SqlLoggerInterceptor());

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
