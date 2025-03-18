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
    //Validation and Transformation
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task<IEnumerable<IExpenseTransferObject>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();

            List<ExpenseDTO> expenseDTOs = new List<ExpenseDTO>();
            foreach (var e in expenses)
            {
                expenseDTOs.Add(new ExpenseDTO(e));
            }

            return expenseDTOs;
        }
        public async Task<IExpenseTransferObject?> GetExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null) return null;

            ExpenseDTO expenseDTO = new(expense);

            return expenseDTO;
        }

        public async Task<bool> AddExpenseAsync(IExpenseTransferObject expenseDTO)
        {
            if (expenseDTO.Id < 0 || expenseDTO.Value < 0 || string.IsNullOrEmpty(expenseDTO.Description))
                return false;

            var expense = new Expense();
            expense.Value = expenseDTO.Value;
            expense.Description = expenseDTO.Description;

            if(expenseDTO.CategoryId != -1)
            {
                await _expenseRepository.AddWithCategoryAsync(expense, expenseDTO.CategoryId);
                return true;
            }

            await _expenseRepository.AddAsync(expense);

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(IExpenseTransferObject expenseDTO)
        {

            if (expenseDTO.Id < 0 || expenseDTO.Value < 0 || string.IsNullOrEmpty(expenseDTO.Description))
                return false;

            var expense = new Expense(expenseDTO.Id, expenseDTO.Value, expenseDTO.Description);

            if (expenseDTO.CategoryId != -1)
            {
                await _expenseRepository.UpdateWithCategoryAsync(expense, expenseDTO.CategoryId);
                return true;
            }

            if (expenseDTO.CategoryName != "-1")
            {
                await _expenseRepository.UpdateAndDeleteCategoryAsync(expense, expenseDTO.CategoryName);
                return true;
            }

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
