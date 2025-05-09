using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppDAL.Entities;
using FluentValidation;
using AutoMapper;

namespace ExpenseWebAppBLL.Services
{
    //Validation
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IValidator<IExpenseTransferObject> _validator;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository,
            IValidator<IExpenseTransferObject> expenseValidator,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _validator = expenseValidator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ExpenseReadDTO>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();

            var expenseDtos = _mapper.Map<List<ExpenseReadDTO>>(expenses);

            return expenseDtos;
        }
        public async Task<IEnumerable<ExpenseReadDTO>> GetAllDeletedExpensesByUserIdAsync(int id)
        {
            var expenses = await _expenseRepository.GetAllDeletedByUserIdAsync(id);

            var expenseDtos = _mapper.Map<List<ExpenseReadDTO>>(expenses);

            return expenseDtos;
        }
        public async Task<ExpenseReadDTO?> GetExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null) return null;

            var expenseDTO = _mapper.Map<ExpenseReadDTO>(expense);

            return expenseDTO;
        }

        public async Task<bool> AddExpenseAsync(ExpenseCreateDTO expenseDTO)
        {
            var validationResult = await _validator.ValidateAsync(expenseDTO);
            if (!validationResult.IsValid) return false;

            var expense = _mapper.Map<Expense>(expenseDTO);

            if(expenseDTO.CategoryId != -1)
            {
                await _expenseRepository.AddWithCategoryAsync(expense, expenseDTO.CategoryId);
                await _expenseRepository.SaveChangesAsync();
                return true;
            }

            await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateExpenseAsync(ExpenseUpdateDTO expenseDTO)
        {
            var validationResult = await _validator.ValidateAsync(expenseDTO);

            if (!validationResult.IsValid) return false;

            var expense = _mapper.Map<Expense>(expenseDTO);

            if (expenseDTO.CategoryId != -1)
            {
                await _expenseRepository.UpdateAndAddCategoryAsync(expense, expenseDTO.CategoryId);
                await _expenseRepository.SaveChangesAsync();
                return true;
            }

            if (expenseDTO.CategoryName != "-1")
            {
                await _expenseRepository.UpdateAndDeleteCategoryAsync(expense, expenseDTO.CategoryName);
                await _expenseRepository.SaveChangesAsync();
                return true;
            }

            await _expenseRepository.UpdateAsync(expense);
            await _expenseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            if (id < 0) return false;

            await _expenseRepository.DeleteAsync(id);
            await _expenseRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> HardDeleteExpenseAsync(int id)
        {
            if (id < 0) return false;

            await _expenseRepository.HardDeleteAsync(id);
            return true;
        }

        public async Task<bool> RestoreExpenseAsync(int id)
        {
            if (id < 0) return false;

            await _expenseRepository.RestoreAsync(id);
            return true;
        }
    }
}
