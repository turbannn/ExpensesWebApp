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
    //Validation
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

            await _expenseRepository.AddAsync(expenseDTO);

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(IExpenseTransferObject expenseDTO)
        {

            if (expenseDTO.Id < 0 || expenseDTO.Value < 0 || expenseDTO.Description == null)
                return false;

            await _expenseRepository.UpdateAsync(expenseDTO);
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
