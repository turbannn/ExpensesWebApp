using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;

namespace ExpenseWebAppDAL.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly WebAppContext _context;

        public ExpenseRepository(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses.Include(e => e.CategoriesList).ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.Include(e => e.CategoriesList).FirstOrDefaultAsync(e => e.Id == id);
        }



        public async Task AddAsync(Expense entityToAdd)
        {

            await _context.Expenses.AddAsync(entityToAdd);

            await _context.SaveChangesAsync();
        }


        public async Task AddWithCategoryAsync(Expense entity, int categoryId = -1)
        {
#pragma warning disable CS8604
            Expense expense = new Expense(entity.Id, entity.Value, entity.Description);
#pragma warning restore CS8604

            expense.CreationDate = DateTime.Now;

            if (categoryId != -1)
            {
                expense.CategoriesList = new List<Category>();

                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

                if (category != null)
                {
                    expense.CategoriesList.Add(category);
                    expense.Categories = category.Name + "; ";
                }
            }

            await _context.Expenses.AddAsync(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense entity)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .FirstAsync(e => e.Id == entity.Id);

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            _context.Expenses.Update(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateWithCategoryAsync(Expense entity, int categoryId)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .FirstAsync(e => e.Id == entity.Id);

            expense.CategoriesList ??= new List<Category>();

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            var category = await _context.Categories.FirstAsync(c => c.Id == categoryId);

            expense.CategoriesList.Add(category);

            _context.Expenses.Update(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAndDeleteCategoryAsync(Expense entity, string categoryName)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .FirstAsync(e => e.Id == entity.Id);

            expense.CategoriesList ??= new List<Category>();

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName.Trim());

            if (category != null)
            {
                expense.CategoriesList.Remove(category);
            }

            _context.Expenses.Update(expense);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}
