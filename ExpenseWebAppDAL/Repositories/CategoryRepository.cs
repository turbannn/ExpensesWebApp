﻿using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebAppContext _context;

        public CategoryRepository(WebAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            await _context.Categories.Where(c => c.Id == category.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Name, category.Name)); 
        }
        public async Task DeleteAsync(int id)
        {
            var delete = await _context.Categories.SingleAsync(e => e.Id == id);
            _context.Remove(delete);

            await _context.SaveChangesAsync();
        }
    }
}