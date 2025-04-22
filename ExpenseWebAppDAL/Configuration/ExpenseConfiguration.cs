using ExpenseWebAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Configuration
{
    internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(e => e.CategoriesList).WithMany(c => c.Expenses);

            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP()");

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
