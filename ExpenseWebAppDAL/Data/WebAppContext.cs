using ExpenseWebAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseWebAppDAL.Configuration;
using ExpenseWebAppDAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace ExpenseWebAppDAL.Data
{
    public class WebAppContext : DbContext
    {
        private static readonly string LogFilePath = "efcore_sql_logs.txt";
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
            optionsBuilder.LogTo(AppendLogToFile, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        private static void AppendLogToFile(string logMessage)
        {
            try
            {
                using (var writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {logMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }
    }
}
