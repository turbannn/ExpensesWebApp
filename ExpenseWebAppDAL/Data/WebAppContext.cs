using ExpenseWebAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpenseWebAppDAL.Data
{
    public class WebAppContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        public WebAppContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Expenses
            modelBuilder.Entity<Expense>().HasKey(c => c.Id);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Value)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .Property(e => e.Description)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .HasMany(e => e.CategoriesList)
                .WithMany(c => c.Expenses);

            modelBuilder.Entity<Expense>()
                .Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP()");

            //Categories
            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(35);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Expenses)
                .WithMany(e => e.CategoriesList);

        }
    }
}
