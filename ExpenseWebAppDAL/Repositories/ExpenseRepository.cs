using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppDAL.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MimeKit.Cryptography;

namespace ExpenseWebAppDAL.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly WebAppContext _context;
        private IDbContextTransaction? _transaction;

        public ExpenseRepository(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .AsNoTracking()
                .Include(e => e.User)
                .Include(e => e.CategoriesList)
                .ToListAsync();
        }
        public async Task<IEnumerable<Expense>> GetAllDeletedByUserIdAsync(int userId)
        {
            var a = await _context.Expenses
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(e => e.UserId == userId && e.IsDeleted)
                .Include(e => e.CategoriesList)
                .ToListAsync();

            return a;
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Include(e => e.User)
                .Include(e => e.CategoriesList)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        //SaveChangesAsync() call needed
        public async Task AddAsync(Expense entityToAdd)
        {
            await _context.Expenses.AddAsync(entityToAdd);
        }

        //SaveChangesAsync() call needed
        public async Task AddWithCategoryAsync(Expense entity, int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category != null)
            {
                entity.CategoriesList = new List<Category> { category };
            }

            await _context.Expenses.AddAsync(entity);

            var tracked = _context.ChangeTracker.Entries();
            foreach (var entry in tracked)
            {
                Console.WriteLine(entry.Entity + " " + entry.State);
            }
        }

        public async Task UpdateAsync(Expense entity)
        {
            await _context.Expenses
                .Where(e => e.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.Value, entity.Value)
                    .SetProperty(e => e.Description, entity.Description));
        }

        //SaveChangesAsync() call needed
        public async Task UpdateAndAddCategoryAsync(Expense entity, int categoryId)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .SingleAsync(e => e.Id == entity.Id);

            expense.CategoriesList ??= new List<Category>();

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            var category = await _context.Categories.FirstAsync(c => c.Id == categoryId);

            expense.CategoriesList.Add(category);
        }

        //SaveChangesAsync() call needed
        public async Task UpdateAndDeleteCategoryAsync(Expense entity, string categoryName)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .SingleAsync(e => e.Id == entity.Id);

            expense.CategoriesList ??= new List<Category>();

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName.Trim());

            if (category != null)
            {
                expense.CategoriesList.Remove(category);
            }
        }

        //SaveChangesAsync() call needed
        public async Task DeleteAsync(int id)
        {
            var delete = await _context.Expenses.FirstAsync(e => e.Id == id);
            _context.Remove(delete);
        }

        //NO NEED to call SaveChangesAsync()
        public async Task HardDeleteAsync(int id)
        {
            await _context.Expenses.IgnoreQueryFilters().Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task RestoreAsync(int id)
        {
            await _context.Expenses
                .IgnoreQueryFilters()
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.IsDeleted, false)
                    .SetProperty(e => e.DeletedAt, DateTimeOffset.MinValue));
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
                await _transaction.RollbackAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
