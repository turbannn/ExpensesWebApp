﻿using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(IExpenseTransferObject entity)
        {
#pragma warning disable CS8604
            Expense expense = new Expense(entity.Id, entity.Value, entity.Description);
#pragma warning restore CS8604

            expense.CreationDate = DateTime.Now;

            if (entity.CategoryId != -1)
            {
                expense.CategoriesList = new List<Category>();

                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == entity.CategoryId);

                if (category != null)
                {
                    expense.CategoriesList.Add(category);
                    expense.Categories = category.Name + "; ";
                }
            }

            await _context.Expenses.AddAsync(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IExpenseTransferObject entity)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .FirstAsync(e => e.Id == entity.Id);

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            _context.Expenses.Update(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateWithCategoryAsync(IExpenseTransferObject entity)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .FirstAsync(e => e.Id == entity.Id);

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            var category = await _context.Categories.FirstAsync(c => c.Id == entity.CategoryId);

#pragma warning disable CS8602
            expense.CategoriesList.Add(category);
#pragma warning restore CS8602

            _context.Expenses.Update(expense);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAndDeleteCategoryAsync(IExpenseTransferObject entity)
        {
            var expense = await _context.Expenses
                .Include(e => e.CategoriesList)
                .FirstAsync(e => e.Id == entity.Id);

            expense.Value = entity.Value;
            expense.Description = entity.Description;

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == entity.CategoryName);

            if (category != null)
            {
#pragma warning disable CS8602
                expense.CategoriesList.Remove(category);
#pragma warning restore CS8602
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
