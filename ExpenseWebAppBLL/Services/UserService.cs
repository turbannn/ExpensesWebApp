using ExpenseWebAppDAL.Entities;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppBLL.DTOs.UserDTOs;
using ExpenseWebAppBLL.Interfaces;
using FluentValidation;
using AutoMapper;

namespace ExpenseWebAppBLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<IUserTransferObject> _validator;
        private readonly IMapper _mapper;

        public UserService(IUserRepository expenseRepository,
            IValidator<IUserTransferObject> expenseValidator,
            IMapper mapper)
        {
            _userRepository = expenseRepository;
            _validator = expenseValidator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserReadDTO>> GetAllExpensesAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = _mapper.Map<List<UserReadDTO>>(users);

            return userDtos;
        }
        public async Task<UserReadDTO?> GetExpenseByIdAsync(int id)
        {
            if (id < 0) return null;

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            var userReadDto = _mapper.Map<UserReadDTO>(user);

            return userReadDto;
        }

        public async Task<bool> AddExpenseAsync(UserCreateDTO userCreateDto)
        {
            var validationResult = await _validator.ValidateAsync(userCreateDto);
            if (!validationResult.IsValid) return false;

            var expense = _mapper.Map<User>(userCreateDto);

            await _userRepository.AddAsync(expense);

            return true;
        }

        public async Task<bool> UpdateExpenseAsync(UserUpdateDTO userUpdateDto)
        {
            var validationResult = await _validator.ValidateAsync(userUpdateDto);
            if (!validationResult.IsValid) return false;

            var expense = _mapper.Map<User>(userUpdateDto);
            try
            {
                await _userRepository.UpdateAsync(expense);
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine("ERROR: User NOT FOUND");
                Console.WriteLine(exception.ToString());
                return false;
            }
            
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {

            if (id < 0) return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
