using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;

namespace ExpenseWebAppBLL.Services
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository repository)
        {
            _expenseRepository = repository;
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

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            if (expense.Id < 0 || expense.Value < 0 || string.IsNullOrEmpty(expense.Description))
                return false;

            expense.CreationDate = DateTime.Now;

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
