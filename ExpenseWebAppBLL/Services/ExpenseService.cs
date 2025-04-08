using ExpenseWebAppBLL.DTOs.ExpenseDTOs;
using ExpenseWebAppBLL.Interfaces;
using ExpenseWebAppBLL.Mappers;
using ExpenseWebAppDAL.Interfaces;
using FluentValidation;

namespace ExpenseWebAppBLL.Services
{
    //Validation
    public class ExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseMapper _expenseMapper;
        private readonly IValidator<IExpenseTransferObject> _validator;
        public ExpenseService(IExpenseRepository expenseRepository, IValidator<IExpenseTransferObject> expenseValidator)
        {
            _expenseRepository = expenseRepository;
            _validator = expenseValidator;
            _expenseMapper = new ExpenseMapper();
        }
        public async Task<IEnumerable<IExpenseTransferObject>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllAsync();

            List<ExpenseReadDTO> expenseDTOs = new List<ExpenseReadDTO>();

            foreach (var e in expenses)
            {
                if(_expenseMapper.ToReadDTO(e) is ExpenseReadDTO rDTO)
                    expenseDTOs.Add(rDTO); //really bad code, need refactor :)
            }

            return expenseDTOs;
        }
        public async Task<IExpenseTransferObject?> GetReadExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null) return null;

            var expenseDTO = _expenseMapper.ToReadDTO(expense); //+ :)

            return expenseDTO;
        }

        public async Task<IExpenseTransferObject?> GetUpdateExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            var expense = await _expenseRepository.GetByIdAsync(id);

            if (expense == null) return null;

            var expenseDTO = _expenseMapper.ToUpdateDTO(expense); //+ :)

            return expenseDTO;
        }

        public async Task<bool> AddExpenseAsync(ExpenseCreateDTO expenseDTO)
        {
            var validationResult = await _validator.ValidateAsync(expenseDTO);
            if (!validationResult.IsValid) return false;

            var expense = _expenseMapper.ToEntity(expenseDTO);

            if(expenseDTO.CategoryId != -1)
            {
                await _expenseRepository.AddWithCategoryAsync(expense, expenseDTO.CategoryId);
                return true;
            }

            await _expenseRepository.AddAsync(expense);

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(ExpenseUpdateDTO expenseDTO)
        {
            var validationResult = await _validator.ValidateAsync(expenseDTO);

            if (!validationResult.IsValid) return false;


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
