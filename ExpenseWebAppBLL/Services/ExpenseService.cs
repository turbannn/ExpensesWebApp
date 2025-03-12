using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;

namespace ExpenseWebAppBLL.Services
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ExpenseService(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }
        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddExpenseAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO.Id < 0 || expenseDTO.Value < 0 || string.IsNullOrEmpty(expenseDTO.Description))
                return false;

            Expense expense = new Expense();

            expense.Id = expenseDTO.Id;
            expense.Value = expenseDTO.Value;
            expense.Description = expenseDTO.Description;
            expense.CreationDate = DateTime.Now;

            if (expenseDTO.CategoryId != -1)
            {
                expense.CategoriesList = new List<Category>();
                var category = _categoryRepository.GetByIdAsync(expenseDTO.CategoryId).Result;
                if (category != null)
                {
                    expense.CategoriesList.Add(category);
                }
            }

            await _expenseRepository.AddAsync(expense);

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense)
        {

            if (expense.Id < 0 || expense.Value < 0)
                return false;

            await _expenseRepository.UpdateAsync(expense);
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {

            if (id < 0) return false;

            await _expenseRepository.DeleteAsync(id);
            return true;
        }
    }
}
