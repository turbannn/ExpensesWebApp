using ExpenseWebAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseWebAppDAL.Configuration;
using ExpenseWebAppDAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpenseWebAppDAL.Data
{
    public class WebAppContext(DbContextOptions options) : DbContext(options)
    {
        private static readonly string LogFilePath = "ef_sql_logs.txt";
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

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
            optionsBuilder.LogTo(AppendLogToFile, LogLevel.Debug);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration()); 
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        private static void AppendLogToFile(string logMessage)
        {
            try
            {
                using (var writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine("======================================================");
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {logMessage}");
                    writer.WriteLine("======================================================");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging error: {ex.Message}");
            }
        }
    }
}
