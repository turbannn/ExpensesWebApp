using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppBLL.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }
        public async Task<IEnumerable<Category>> GetAllExpensesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<Category?> GetExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddExpenseAsync(Category category)
        {
            if (category.Id < 0)
                return false;

            await _categoryRepository.AddAsync(category);
            return true;
        }

        public async Task<bool> UpdateExpenseAsync(Category category)
        {

            if (category.Id < 0)
                return false;

            await _categoryRepository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {

            if (id < 0) return false;

            await _categoryRepository.DeleteAsync(id);
            return true;
        }
    }
}
