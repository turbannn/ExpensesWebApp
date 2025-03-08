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
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task AddAsync(Expense entity)
        {
            await _context.Expenses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense entity)
        {
            _context.Expenses.Update(entity);
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
