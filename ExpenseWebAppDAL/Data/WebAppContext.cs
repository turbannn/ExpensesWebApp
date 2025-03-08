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
    }
}
