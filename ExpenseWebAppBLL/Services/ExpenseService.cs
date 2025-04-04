using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseWebAppBLL.DTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppBLL.Mappers;
using ExpenseWebAppDAL.Interfaces;

namespace ExpenseWebAppBLL.Services
{
    //Validation
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseMapper _expenseMapper;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _expenseMapper = new ExpenseMapper();
        }
        public async Task<IEnumerable<IExpenseTransferObject>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();

            List<IExpenseTransferObject> expenseDTOs = new List<IExpenseTransferObject>();

            foreach (var e in expenses)
            {
                if(_expenseMapper.ToDTO(e) is ExpenseDTO eDTO)
                    expenseDTOs.Add(eDTO);
            }

            return expenseDTOs;
        }
        public async Task<IExpenseTransferObject?> GetExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null) return null;

            var expenseDTO = _expenseMapper.ToDTO(expense);

            return expenseDTO;
        }

        public async Task<bool> AddExpenseAsync(IExpenseTransferObject expenseDTO)
        {
            if (expenseDTO.Id < 0 || expenseDTO.Value < 0 || string.IsNullOrEmpty(expenseDTO.Description))
                return false;

            var expense = _expenseMapper.ToEntity(expenseDTO);

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

            var expense = _expenseMapper.ToEntity(expenseDTO);

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
