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
        private readonly IRepository<Expense> _expenseRepository;

        public ExpenseService(IRepository<Expense> repository)
        {
            _expenseRepository = repository;
        }
        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }
        public async Task<Expense?> GetExpenseByIdAsync(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            if (expense.Id < 0 || expense.Value < 0)
                return false;

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
            await _expenseRepository.DeleteAsync(id);
            return true;
        }
    }
}
