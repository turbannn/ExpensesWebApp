using ExpenseWebAppDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseWebAppDAL.Data.Configuration;

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
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
