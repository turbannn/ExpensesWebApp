using ExpenseWebAppDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseWebAppDAL.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(35);

            builder.HasMany(c => c.Expenses).WithMany(e => e.CategoriesList);

            builder.HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
